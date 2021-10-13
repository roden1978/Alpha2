﻿using System;
using System.Collections.Generic;
using Common;
using Input;
using PlayerScripts;
using PlayerScripts.States;
using UnityEngine;

namespace GameScripts
{
    [RequireComponent(typeof(StateMachine), typeof(DevicesInput))]
    public class Crowbar : MonoBehaviour
    {
       
        private StateMachine _stateMachine;
        private DevicesInput _input;
        private Player _player;
        private PlayerView _playerView;
        private IFlipView _flipView;
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _player = FindObjectOfType<Player>();
            _playerView = _player.GetComponentInChildren<PlayerView>();
            _flipView = new FlipView(_playerView);
        }

        private void Start()
        {
            _rigidbody = _player.GetComponent<Rigidbody2D>();
            _stateMachine = GetComponent<StateMachine>();
            _input = GetComponent<DevicesInput>();
            
            var player = _player.gameObject;
            _stateMachine.Initialize(new Dictionary<Type, BaseState>
            {
                {typeof(IdleState), new IdleState(player)},
                {typeof(WalkState), new WalkState(player)},
                {typeof(JumpState), new JumpState(player)}
            });
        }

        private void Update()
        {
            FLip();
        }

        private void FixedUpdate()
        {
            Move();
            Jump();
        }
        
        private void Move()
        {
            if (_player.StayOnGround() && _input.Direction != 0)
            {
                if (Mathf.Abs(_rigidbody.velocity.magnitude) > _player.MaxVelocity) return;
                _rigidbody.AddForce(new Vector2(_input.Direction, 0) * _player.Speed, ForceMode2D.Impulse);
            }
        } 

        private void Jump()
        {
            if (_player.StayOnGround() && _input.Jump != 0)
            {
                ResetYVelocity();
                var jumpForce = new Vector2(0, _input.Jump) * _player.JumpForce;
                _rigidbody.AddForce(jumpForce, ForceMode2D.Impulse);
            }
        }
        
        private void FLip()
        {
            _flipView.FLippingPlayerView(_input.Direction);
        }

        private void ResetYVelocity()
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
        }
    }
}