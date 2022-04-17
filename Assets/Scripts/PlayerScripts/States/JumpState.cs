using System;
using Common;
using GameObjectsScripts;
using UnityEngine;

namespace PlayerScripts.States
{
    public class JumpState : IState
    {
        private readonly Animator _animator;
        private readonly PlayerStateData _playerStateData;
        private readonly IShowable _groundingFx;
        private readonly IShowable _jumpFx;

        public JumpState(Animator animator, PlayerStateData playerStateData, IShowable groundingFx, IShowable jumpFx)
        {
            _animator = animator;
            _playerStateData = playerStateData;
            _groundingFx = groundingFx;
            _jumpFx = jumpFx;
        }

        public void Enter()
        {
            _animator.SetBool(PlayerAnimationConstants.Jump, true);
            _jumpFx.Show();
        }

        public Type Update()
        {
            if (_playerStateData.IsShoot) 
            {
                _animator.SetBool(PlayerAnimationConstants.JumpThrow, true);
                return typeof(JumpProxyState);
            }
            
            return _playerStateData.IsOnGround ? typeof(JumpProxyState) : typeof(EmptyState);
        }

        public void Exit()
        {
            if (_playerStateData.IsShoot) _playerStateData.IsShoot = false;
            _animator.SetBool(PlayerAnimationConstants.Jump, false);
            _groundingFx.Show();
            _jumpFx.Hide();
        }
    }
}
