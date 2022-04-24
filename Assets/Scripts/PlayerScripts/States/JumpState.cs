using System;
using Common;
using GameObjectsScripts;
using Services.Input;
using UnityEngine;

namespace PlayerScripts.States
{
    public class JumpState : IState
    {
        private readonly Animator _animator;
        private bool _isShoot;
        private readonly IShowable _groundingFx;
        private readonly IShowable _jumpFx;

        public JumpState(Animator animator, IInputService inputService, IShowable groundingFx, IShowable jumpFx)
        {
            
            _animator = animator;
            inputService.OnShoot += () => _isShoot = true;
            inputService.OnStopShoot += () => _isShoot = false;
            _groundingFx = groundingFx;
            _jumpFx = jumpFx;
        }

        public void Enter()
        {
            Debug.Log("JumpState enter");
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
            Debug.Log("JumpState exit");
            if (_isShoot)
            {
                _animator.SetBool(PlayerAnimationConstants.JumpThrow, true);
            }
            _animator.SetBool(PlayerAnimationConstants.Jump, false);
            _groundingFx.Show();
            _jumpFx.Hide();
        }
    }
}
