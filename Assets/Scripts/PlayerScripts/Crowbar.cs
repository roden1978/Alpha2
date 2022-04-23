using System.Threading.Tasks;
using Cinemachine;
using Common;
using Data;
using GameObjectsScripts;
using Infrastructure.Services;
using PlayerScripts.Conditions;
using PlayerScripts.States;
using Services.Input;
using Services.PersistentProgress;
using Services.StaticData;
using StaticData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayerScripts
{
    public class Crowbar : MonoBehaviour, ISavedProgress
    {
        private const int DoubleSingWaitTime = 1200;
        
        [SerializeField] private float _speed;
        [SerializeField] private float _maxVelocity;
        [SerializeField] private float _jumpForce;
        [SerializeField] private Vector2 _damping;

        private StateMachine _stateMachine;
        private IInputService _inputService;
        private IFlipView _flipView;
        private IDipstick _dipstick;
        private Camera _camera;
        private ICinemachineCamera _virtualCamera;
        private IStaticDataService _staticDataService;
        private Player _player;
        private bool _doubleJump;
        private bool _resetVelocity;
        private bool _isShoot;
        private IShowable _footstepFx;
        private IShowable _groundingFx;
        private IShowable _jumpFx;

        private void Awake()
        {
            _stateMachine = new StateMachine();
            _camera = Camera.main;
        }

        public void Construct(Player player, IStaticDataService staticDataService,
                                IShowable footstepFx, IShowable groundingFx, IShowable jumpFx)
        {
            _player = player;
            _footstepFx = footstepFx;
            _groundingFx = groundingFx;
            _jumpFx = jumpFx;
            _staticDataService = staticDataService;
            _dipstick = new Dipstick(_player);
            _flipView = new FlipView(_player.PlayerView);
            _inputService = ServiceLocator.Container.Single<IInputService>();
            _inputService.OnJump += Jump;
            //_inputService.OnShoot += Shoot;

            IState idleState = new IdleState(_isShoot);
            IState walkState = new WalkState(_player.Animator, _isShoot, _footstepFx);
            IState jumpState = new JumpState(_player.Animator, _inputService, _groundingFx, _jumpFx);
            IState idleThrowState = new IdleThrowState(_player.Animator);
            IState jumpThrowState = new JumpThrowState(_player.Animator);
            IState walkThrowState = new WalkThrowState(_player.Animator, _footstepFx);
            IState jumpProxyState = new JumpProxyState();
            IState walkProxyState = new WalkProxyState();
            
            _stateMachine.AddTransition(idleState, jumpState, 
                new IdleToJump(_player.Rigidbody2D, _dipstick, _damping.y));
            _stateMachine.AddTransition(idleState, walkState, 
                new IdleToWalk(_player.Rigidbody2D, _damping.x));
            _stateMachine.AddTransition(idleState, idleThrowState, 
                new IdleToIdleThrow(_inputService));
            _stateMachine.AddTransition(idleThrowState, idleState, 
                new IdleThrowToIdle(_player.Animator));
            _stateMachine.AddTransition(jumpProxyState, idleState, 
                new JumpProxyToIdle(_player.Animator));
            _stateMachine.AddTransition(jumpProxyState, jumpThrowState, 
                new JumpProxyToJumpThrow(_player.Animator));
            _stateMachine.AddTransition(jumpThrowState, jumpState, 
                new JumpThrowToJump(_player.Animator));
            _stateMachine.AddTransition(jumpState, jumpProxyState, 
                new JumpToJumpProxy(_dipstick, _inputService));
            _stateMachine.AddTransition(walkProxyState, idleState, 
                new WalkProxyToIdle(_player.Animator));
            _stateMachine.AddTransition(walkProxyState, walkThrowState, 
                new WalkProxyToWalkThrow(_player.Animator));
            _stateMachine.AddTransition(walkThrowState, walkState, 
                new WalkThrowToWalk(_player.Animator));
            _stateMachine.AddTransition(walkState, idleState, 
                new WalkToIdle(_player.Rigidbody2D, _damping.x));
            _stateMachine.AddTransition(walkState, jumpState, 
                new WalkToJump(_player.Rigidbody2D, _dipstick, _damping.y));
            _stateMachine.AddTransition(walkState, walkProxyState, 
                new WalkToWalkThrow(_inputService));
            
            _stateMachine.SetState(idleState);
        }

        private void Update()
        {
            _stateMachine.Update();
            FLip();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void OnDestroy()
        {
            _inputService.OnJump -= Jump;
            //_inputService.OnShoot -= Shoot;
        }

        private void Move()
        {
            if (StayOnGround() && _inputService.Move() != 0)
            {
                if (Mathf.Abs(_player.Rigidbody2D.velocity.magnitude) > _maxVelocity) return;
                _player.Rigidbody2D.AddForce(new Vector2(_inputService.Move(), 0) * _speed, ForceMode2D.Impulse);
                _resetVelocity = true;
            }
            else if (_inputService.Move() == 0 && _resetVelocity && StayOnGround())
            {
                _player.Rigidbody2D.velocity = Vector2.zero;
                _resetVelocity = false;
            }
        }

        public void Jump()
        {
            if (StayOnGround())
            {
                AddForceToJump();
                DoubleJumpSignShow();
                _doubleJump = true;
            }
            else
            {
                DoubleJump();
            }
        }

        private void DoubleJump()
        {
            bool canJump = Vector2.Dot(_player.Rigidbody2D.velocity, Vector2.up) < 0;

            if (canJump && _doubleJump)
            {
                AddForceToJump();
                _doubleJump = false;
            }
        }

        private void FLip()
        {
            float direction = _inputService.Move();
            _flipView.FLippingPlayerView(direction);
        }

        private void AddForceToJump()
        {
            Vector2 jumpForce = Vector2.up * _jumpForce;
            _player.Rigidbody2D.AddForce(jumpForce, ForceMode2D.Impulse);
        }

        public void Shoot() => 
            _isShoot = true;

        private bool StayOnGround() => 
            _dipstick.Contact();

        private async void DoubleJumpSignShow()
        {
            _player.DoubleJumpSign.Show();
            await Task.Delay(DoubleSingWaitTime);
            _player.DoubleJumpSign.Hide();
        }

        public void UpdateProgress(PlayerProgress playerProgress)
        {
            Vector3Data asVector3Data = _player.transform.position.AsVector3Data();
            playerProgress.WorldData.PositionOnLevel.Position = asVector3Data;
        }

        public void LoadProgress(PlayerProgress playerProgress)
        {
            Vector3Data position = playerProgress.WorldData.PositionOnLevel.Position;
            PositionPlayer(position);
        }

        private async void PositionPlayer(Vector3Data position)
        {
            string levelKey = SceneManager.GetActiveScene().name;
            LevelStaticData levelStaticData = _staticDataService.GetLevelStaticData(levelKey);
            
            Vector3 spawnPoint = levelStaticData.PlayerSpawnPoint;

            if (position.AsVector3() == Vector3.zero)
                _player.transform.position = spawnPoint != Vector3.zero ? spawnPoint : Vector3.zero;
            else
                _player.transform.position = position.AsVector3();
            _virtualCamera = await GetVCamera();
            if (_virtualCamera.Follow == null)
            {
                _virtualCamera.VirtualCameraGameObject.transform.position = position.AsVector3();
                Transform targetTransform = _player.transform;
                _virtualCamera.Follow = targetTransform;
                _virtualCamera.LookAt = targetTransform;
            }
        }

        private async Task<ICinemachineCamera> GetVCamera()
        {
            await Task.Delay(200);
            return _camera.GetComponent<CinemachineBrain>().ActiveVirtualCamera;
        }
    }
}