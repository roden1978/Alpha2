using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cinemachine;
using Common;
using Data;
using Infrastructure.Services;
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
        private PlayerView _playerView;
        private IFlipView _flipView;
        private Rigidbody2D _rigidbody;
        private Animator _animator;
        private IDipstick _dipstick;
        private PlayerStateData _playerStateData;
        private DoubleJumpSign _doubleJumpSign;

        private bool _doubleJump;

        private Camera _camera;
        private ICinemachineCamera _virtualCamera;
        private IStaticDataService _staticDataService;
        private Player _player;

        private void Awake()
        {
            Debug.Log("Awake");
            _stateMachine = new StateMachine();
            _camera = Camera.main;
        }

        public void Construct(Player player, IStaticDataService staticDataService)
        {
            _player = player;
            _staticDataService = staticDataService;
            _dipstick = new Dipstick(_player);
            _playerView = _player.GetComponentInChildren<PlayerView>();
            _doubleJumpSign = _playerView.GetComponentInChildren<DoubleJumpSign>(true);
            _flipView = new FlipView(_playerView);
            _animator = _player.GetComponentInChildren<Animator>();
            _rigidbody = _player.GetComponent<Rigidbody2D>();

            _inputService = ServiceLocator.Container.Single<IInputService>();
            _inputService.OnJump += Jump;
            _inputService.OnShoot += Shoot;

            _playerStateData = new PlayerStateData
            {
                Damping = _damping
            };

            _stateMachine.Initialize(new Dictionary<Type, IState>
            {
                { typeof(IdleState), new IdleState(_rigidbody, _playerStateData) },
                { typeof(WalkState), new WalkState(_rigidbody, _animator, _playerStateData) },
                { typeof(JumpState), new JumpState(_animator, _playerStateData) },
                { typeof(IdleThrowState), new IdleThrowState(_animator) },
                { typeof(JumpThrowState), new JumpThrowState(_animator) },
                { typeof(JumpProxyState), new JumpProxyState(_animator) },
                { typeof(WalkThrowState), new WalkThrowState(_animator) },
                { typeof(WalkProxyState), new WalkProxyState(_animator) }
            });
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
            _inputService.OnShoot -= Shoot;
        }

        private void Move()
        {
            if (StayOnGround() && _inputService.Move() != 0)
            {
                if (Mathf.Abs(_rigidbody.velocity.magnitude) > _maxVelocity) return;
                _rigidbody.AddForce(new Vector2(_inputService.Move(), 0) * _speed, ForceMode2D.Impulse);
            }
        }

        private void Jump()
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
            bool canJump = Vector2.Dot(_rigidbody.velocity, Vector2.up) < 0;

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
            _rigidbody.AddForce(jumpForce, ForceMode2D.Impulse);
        }

        private void Shoot()
        {
            _playerStateData.IsShoot = true;
        }

        private bool StayOnGround()
        {
            bool result = _dipstick.Contact();
            _playerStateData.IsOnGround = result;

            return result;
        }

        private async void DoubleJumpSignShow()
        {
            _doubleJumpSign.Show();
            await Task.Delay(DoubleSingWaitTime);
            _doubleJumpSign.Hide();
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
            Debug.Log(levelKey);
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
            await Task.Delay(100);
            return _camera.GetComponent<CinemachineBrain>().ActiveVirtualCamera;
        }
    }
}