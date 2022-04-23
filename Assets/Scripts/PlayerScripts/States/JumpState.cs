using System;
using Common;
using GameObjectsScripts;
using UnityEngine;

namespace PlayerScripts.States
{
    public class JumpState : IState
    {
        private readonly Animator _animator;
        private bool _isShoot;
        private readonly IShowable _groundingFx;
        private readonly IShowable _jumpFx;

        public JumpState(Animator animator, bool isShoot, IShowable groundingFx, IShowable jumpFx)
        {
            _animator = animator;
            _isShoot = isShoot;
            _groundingFx = groundingFx;
            _jumpFx = jumpFx;
        }

        public void Enter()
        {
            _animator.SetBool(PlayerAnimationConstants.Jump, true);
            _jumpFx.Show();
        }

        public void Update()
        {
            /*if (_playerStateData.IsShoot) 
            {
                
                return typeof(JumpProxyState);
            }*/
            
            //return _playerStateData.IsOnGround ? typeof(JumpProxyState) : typeof(EmptyState);
        }

        public void Exit()
        {
            if (_isShoot)
            {
                _isShoot = false;
                _animator.SetBool(PlayerAnimationConstants.JumpThrow, true);
            }
            _animator.SetBool(PlayerAnimationConstants.Jump, false);
            _groundingFx.Show();
            _jumpFx.Hide();
        }
    }
}
