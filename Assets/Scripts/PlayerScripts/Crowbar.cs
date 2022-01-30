using System;
using System.Collections.Generic;
using Common;
using Infrastructure;
using PlayerScripts.States;
using Services.Input;
using UnityEngine;

namespace PlayerScripts
{
    public class Crowbar : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _maxVelocity;
        [SerializeField] private float _jumpForce;
        [SerializeField] private Vector2 _damping;
        
        private Player _player;
        private StateMachine _stateMachine;
        private IInputService _inputService;
        private PlayerView _playerView;
        private IFlipView _flipView;
        private Rigidbody2D _rigidbody;
        private Animator _animator;
        private IDipstick _dipstick;
        private PlayerStateData _playerStateData;

        private bool _doubleJump;

        private void Awake()
        {
            _player = GetComponent<Player>();
            _stateMachine = new StateMachine();
        }

        private void Start()
        {
            _dipstick = new Dipstick(_player);
            _playerView = _player.GetComponentInChildren<PlayerView>();
            _flipView = new FlipView(_playerView);
            _animator = _player.GetComponentInChildren<Animator>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _inputService = Game.InputService;
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
                _doubleJump = true;
            }
        }

        private void Jump()
        {
            if (StayOnGround())
            {
                AddForceToJump();
                _doubleJump = true;
            }
            else
            {
                DoubleJump();
            }
        }

        private void DoubleJump()
        {
            if (Vector2.Dot(_rigidbody.velocity, Vector2.up) < 0 &&
                _doubleJump)
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
            _playerStateData.IsOnGround = _dipstick.Contact();
            return _playerStateData.IsOnGround;
        }
    }
}