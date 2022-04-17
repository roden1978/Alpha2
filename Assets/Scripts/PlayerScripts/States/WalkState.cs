using System;
using Common;
using GameObjectsScripts;
using UnityEngine;

namespace PlayerScripts.States
{
    
    public class WalkState : IState
    {
        private readonly Animator _animator;
        private readonly Rigidbody2D _rigidbody;
        private readonly PlayerStateData _playerStateData;
        private readonly IShowable _footstepFx;

        public WalkState(Rigidbody2D rigidbody2D, Animator animator, PlayerStateData playerStateData,
            IShowable footstepFx)
        {
            _rigidbody = rigidbody2D;
            _animator = animator;
            _playerStateData = playerStateData;
            _footstepFx = footstepFx;
        }

        public void Enter()
        {
            _animator.SetBool(PlayerAnimationConstants.Walk, true);
            _footstepFx.Show();
        }

       
        public Type Update()
        {
            if (_playerStateData.IsShoot) 
            {
                _animator.SetBool(PlayerAnimationConstants.WalkThrow, true);
                return typeof(WalkProxyState);
            }

            if (!_playerStateData.IsOnGround && _rigidbody.velocity.y > _playerStateData.Damping.y)
                return typeof(JumpState);
            
            return Mathf.Abs(_rigidbody.velocity.x) < _playerStateData.Damping.x ? typeof(IdleState) : typeof(EmptyState);
        }

        public void Exit()
        {
            if (_playerStateData.IsShoot) _playerStateData.IsShoot = false;
            _animator.SetBool(PlayerAnimationConstants.Walk, false);
            _footstepFx.Hide();
        }
    }
}