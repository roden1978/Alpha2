using System;
using System.Collections.Generic;
using Common;
using Input;
using PlayerScripts;
using PlayerScripts.States;
using UnityEngine;

namespace GameScripts
{
    [RequireComponent(typeof(StateMachine))]
    public class Crowbar : MonoBehaviour
    {
        private StateMachine _stateMachine;
        private IPlayerInput _input;
        private Player _player;
        private PlayerView _playerView;
        private IFlipView _flipView;
        private Rigidbody2D _rigidbody;
        private Animator _animator;

        private bool _doubleJump;
        private bool _shoot;

        private void Awake()
        {
            _input = new KeyboardInput();
        }

        private void Start()
        {
            _playerView = _player.GetComponentInChildren<PlayerView>();
            _flipView = new FlipView(_playerView);
            _animator = _player.GetComponentInChildren<Animator>();
            _rigidbody = _player.GetComponent<Rigidbody2D>();
            _stateMachine = GetComponent<StateMachine>();
            
            
            _stateMachine.Initialize(new Dictionary<Type, IState>
            {
                {typeof(IdleState), new IdleState(_player, _rigidbody)},
                {typeof(WalkState), new WalkState(_player, _rigidbody, _animator)},
                {typeof(JumpState), new JumpState(_player, _animator)},
                {typeof(IdleThrowState), new IdleThrowState(_animator)},
                {typeof(JumpThrowState), new JumpThrowState(_animator)},
                {typeof(JumpProxyState), new JumpProxyState(_animator)},
                {typeof(WalkThrowState), new WalkThrowState(_animator)},
                {typeof(WalkProxyState), new WalkProxyState(_animator)}
            });
        }

        private void Update()
        {
            FLip();
            Shoot();
        }

        private void FixedUpdate()
        {
            Move();
            Jump();
            DoubleJump();
        }

        public void Inject(Player player)
        {
            _player = player;
        }

      
        private void Move()
        {
            if (_player.StayOnGround() && _input.Move() != 0)
            {
                if (Mathf.Abs(_rigidbody.velocity.magnitude) > _player.MaxVelocity) return;
                _rigidbody.AddForce(new Vector2(_input.Move(), 0) * _player.Speed, ForceMode2D.Impulse);
                _doubleJump = true;
            }
        } 

        private void Jump()
        {
            if (_player.StayOnGround() && _input.Jump() != 0)
            {
                ResetYVelocity();
                AddForceToJump();
                _doubleJump = true;
            }
            
        }

        private void DoubleJump()
        {
            if (Vector2.Dot(_rigidbody.velocity, Vector2.up) < 0 && 
                _input.Jump() != 0 && 
                _doubleJump)
            {
                AddForceToJump();
                _doubleJump = false;
            }
        }

        private void FLip()
        {
            _flipView.FLippingPlayerView(_input.Move());
        }

        private void ResetYVelocity()
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
        }

        private void AddForceToJump()
        {
            var jumpForce = new Vector2(0, _input.Jump()) * _player.JumpForce;
            _rigidbody.AddForce(jumpForce, ForceMode2D.Impulse);
        }

        private void Shoot()
        {
           if(_input.Shoot() > 0 && !_shoot)
           {
               _player.InvokeShootAction();
               _shoot = true;
           }

           if (_input.Shoot() == 0 && _shoot)
           {
               _shoot = false;
           }
        }
        
    }
}