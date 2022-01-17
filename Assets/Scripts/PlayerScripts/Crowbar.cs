using System;
using System.Collections.Generic;
using Common;
using Infrastructure;
using Input;
using PlayerScripts.States;
using Services.Input;
using UnityEngine;

namespace PlayerScripts
{
    [RequireComponent(typeof(StateMachine))]
    public class Crowbar : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _maxVelocity;
        [SerializeField] private float _jumpForce;
        //[SerializeField] private float _xMoveDamping = 0.3f;
        //[SerializeField] private float _yMoveDamping = 0.3f;
        [SerializeField] private Vector2 _damping;
        
        //public event Action OnShoot;
        
        private Player _player;
        private StateMachine _stateMachine;
        private IInputService _inputService;
        private PlayerView _playerView;
        private IFlipView _flipView;
        private Rigidbody2D _rigidbody;
        private Animator _animator;
        private IDipstick _dipstick;

        private bool _doubleJump;
        //private bool _shoot;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void Start()
        {
            _dipstick = new Dipstick(_player);
            _playerView = _player.GetComponentInChildren<PlayerView>();
            _flipView = new FlipView(_playerView);
            _animator = _player.GetComponentInChildren<Animator>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _stateMachine = GetComponent<StateMachine>();
            _inputService = Game.InputService;
            _inputService.OnJump += Jump;
            _inputService.OnShoot += Shoot;
            PlayerStateData.Damping = _damping;

            _stateMachine.Initialize(new Dictionary<Type, IState>
            {
                { typeof(IdleState), new IdleState(_rigidbody) },
                { typeof(WalkState), new WalkState(_rigidbody, _animator) },
                { typeof(JumpState), new JumpState(_animator) },
                { typeof(IdleThrowState), new IdleThrowState(_animator) },
                { typeof(JumpThrowState), new JumpThrowState(_animator) },
                { typeof(JumpProxyState), new JumpProxyState(_animator) },
                { typeof(WalkThrowState), new WalkThrowState(_animator) },
                { typeof(WalkProxyState), new WalkProxyState(_animator) }
            });
        }

        private void Update()
        {
            FLip();
        }

        private void FixedUpdate()
        {
            Move();
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
                //ResetYVelocity();
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

        private void ResetYVelocity()
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
        }

        private void AddForceToJump()
        {
            Vector2 jumpForce = Vector2.up * _jumpForce;
            _rigidbody.AddForce(jumpForce, ForceMode2D.Impulse);
        }

        private void Shoot()
        {
            PlayerStateData.IsShoot = true;
        }

       // public float XDamping => _xMoveDamping;
        //public float YDamping => _yMoveDamping;
        

        private bool StayOnGround()
        {
            PlayerStateData.IsOnGround = _dipstick.Contact();
            return PlayerStateData.IsOnGround;
        }

        
    }
    public static class PlayerStateData
    {
        public static bool IsShoot { get; set; }
        public static bool IsOnGround { get; set; }
        public static Vector2 Damping { get; set; }
    }
}