using System;
using Common;
using UnityEngine;

namespace PlayerScripts.States
{
    public class JumpState : IState
    {
        private readonly Animator _animator;
        private readonly PlayerStateData _playerStateData;
        public JumpState(Animator animator, PlayerStateData playerStateData)
        {
            _animator = animator;
            _playerStateData = playerStateData;
        }

        public void Enter()
        {
            //_player.OnShoot += Shoot;
            _animator.SetBool(PlayerAnimationConstants.Jump, true);
        }

        public Type Tick()
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
        }
    }
}
