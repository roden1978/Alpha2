using System;
using System.Collections;
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
        [SerializeField] private float _speed;
        [SerializeField] private float _maxVelocity;
        [SerializeField] private float _jumpForce;
        [SerializeField] [Range(1f, 2f)] private float _doubleSingWaitTime;
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
        private IShowable _footstepFx;
        private IShowable _groundingFx;
        private IShowable _jumpFx;
        private IShowable _jumpSoundFx;

        private void Awake()
        {
            _stateMachine = new StateMachine();
            _camera = Camera.main;
        }

        public void Construct(Player player, IStaticDataService staticDataService,
                                IShowable footstepFx, IShowable groundingFx, IShowable jumpFx, IShowable jumpSoundFx)
        {
            _player = player;
            _footstepFx = footstepFx;
            _groundingFx = groundingFx;
            _jumpFx = jumpFx;
            _jumpSoundFx = jumpSoundFx;
            _staticDataService = staticDataService;
            _dipstick = new Dipstick(_player, _player, player.GetComponentInChildren<SpriteRenderer>().sprite);
            _flipView = new FlipView(_player.PlayerView);
            _inputService = ServiceLocator.Container.Single<IInputService>();
            _inputService.OnJump += Jump;

            IState idleState = new IdleState(player.Animator);
            IState walkState = new WalkState(_player.Animator, _footstepFx);
            IState jumpState = new JumpState(_player.Animator, _dipstick, _groundingFx, _jumpFx);
            IState idleThrowState = new IdleThrowState(_player.Animator);
            IState jumpThrowState = new JumpThrowState(_player.Animator);
            IState walkThrowState = new WalkThrowState(_player.Animator, _footstepFx);
            
            _stateMachine.AddTransition(idleState, jumpState, 
                new IdleToJump(_player.Rigidbody2D, _dipstick, _damping.y));
            _stateMachine.AddTransition(idleState, walkState, 
                new IdleToWalk(_player.Rigidbody2D, _damping.x, _inputService));
            _stateMachine.AddTransition(idleState, idleThrowState, 
                new IdleToIdleThrow(_inputService, _player.Rigidbody2D, _damping.x));
            _stateMachine.AddTransition(idleThrowState, idleState, 
                new IdleThrowToIdle(_player.Animator));
            _stateMachine.AddTransition(jumpState, jumpThrowState, 
                new JumpToJumpThrow(_dipstick, _inputService));
            _stateMachine.AddTransition(jumpThrowState, jumpState, 
                new JumpThrowToJump(_player.Animator));
            _stateMachine.AddTransition(jumpState, idleState, 
                new JumpToIdle(_dipstick));
            _stateMachine.AddTransition(walkThrowState, walkState, 
                new WalkThrowToWalk(_player.Animator));
            _stateMachine.AddTransition(walkState, idleState, 
                new WalkToIdle(_player.Rigidbody2D, _damping.x));
            _stateMachine.AddTransition(walkState, jumpState, 
                new WalkToJump(_player.Rigidbody2D, _dipstick, _damping.y));
            _stateMachine.AddTransition(walkState, walkThrowState, 
                new WalkToWalkThrow(_inputService, _player.Rigidbody2D, _damping.x));
            
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
                PlayJumpSoundFx();
                StartCoroutine(DoubleJumpSignShow());
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
                PlayJumpSoundFx();
                _doubleJump = false;
            }
        }

        private void FLip()
        {
            float direction = _inputService.Move();
            float lookDirection = _flipView.FLippingPlayerView(direction);
            _player.LookDirection = Convert.ToInt32(lookDirection);
        }

        private void AddForceToJump()
        {
            Vector2 jumpForce = Vector2.up * _jumpForce;
            _player.Rigidbody2D.AddForce(jumpForce, ForceMode2D.Impulse);
        }

        public void Shoot() => 
            _inputService.Shoot();

        public void StopShoot() => 
            _inputService.StopShoot();

        private bool StayOnGround() => 
            _dipstick.Contact();

        private  IEnumerator DoubleJumpSignShow()
        {
            _player.DoubleJumpSign.Show();
            yield return new WaitForSeconds(_doubleSingWaitTime);
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
            while (_camera.GetComponent<CinemachineBrain>().ActiveVirtualCamera == null)
                await Task.Yield();    
            
            return _camera.GetComponent<CinemachineBrain>().ActiveVirtualCamera;
        }

        private void PlayJumpSoundFx()
        {
            _jumpSoundFx.Show();
        }
    }
}