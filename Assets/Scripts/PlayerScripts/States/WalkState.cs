﻿using System;
using Common;
using UnityEngine;

namespace PlayerScripts.States
{
    
    public class WalkState : BaseState
    {
        private readonly Animator _animator;
        private readonly Rigidbody2D _rigidbody;
        private readonly Player _player;
        private bool _isShoot;

        public WalkState(GameObject player) : base(player)
        {
            _rigidbody = player.GetComponent<Rigidbody2D>();
            _animator = player.GetComponentInChildren<Animator>();
            _player = player.GetComponent<Player>();
        }

        public override void Enter()
        {
            _animator.SetBool(Walk, true);
            _player.OnShoot += Shoot;
        }

        private void Shoot()
        {
            _isShoot = true;
        }

        public override Type Tick()
        {
            if(Mathf.Abs(_rigidbody.velocity.x) < _player.XMoveDamping) 
                return typeof(IdleState); 
            return _isShoot ? typeof(IdleThrowState) : typeof(EmptyState);
        }

        public override Type FixedTick()
        {
            return !_player.StayOnGround() ? typeof(JumpState) : typeof(EmptyState);
        }
        

        public override void Exit()
        {
            _isShoot = false;
            _player.OnShoot -= Shoot;
            _animator.SetBool(Walk, false);
        }
    }
}