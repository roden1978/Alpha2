using System;
using Common;
using UnityEngine;

namespace PlayerScripts.States
{
    
    public class WalkState : IState
    {
        private readonly Animator _animator;
        private readonly Rigidbody2D _rigidbody;
        private readonly Player _player;
        private bool _isShoot;

        public WalkState(Player player, Rigidbody2D rigidbody2D, Animator animator)
        {
            _player = player;
            _rigidbody = rigidbody2D;
            _animator = animator;
        }

        public void Enter()
        {
            _player.OnShoot += Shoot;
            _animator.SetBool(PlayerAnimationConstants.Walk, true);
        }

        private void Shoot()
        {
            _isShoot = true;
        }

        public Type Tick()
        {
            if(_isShoot)
            {
                _animator.SetBool(PlayerAnimationConstants.WalkThrow, true);
                return typeof(WalkProxyState);
            }

            return Mathf.Abs(_rigidbody.velocity.x) < _player.XMoveDamping ? typeof(IdleState) : typeof(EmptyState);
        }

        public Type FixedTick()
        {
            return !_player.StayOnGround() ? typeof(JumpState) : typeof(EmptyState);
        }
        

        public void Exit()
        {
            _isShoot = false;
            _player.OnShoot -= Shoot;
            _animator.SetBool(PlayerAnimationConstants.Walk, false);
        }
    }
}