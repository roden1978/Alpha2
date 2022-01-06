using System;
using Common;
using UnityEngine;

namespace PlayerScripts.States
{
    public class JumpState : IState
    {
        private readonly Animator _animator;
        //private readonly Player _player;
        //private bool _isShoot;

        public JumpState(Animator animator)
        {
            //_player = player;
            _animator = animator;
        }

        public void Enter()
        {
            //_player.OnShoot += Shoot;
            _animator.SetBool(PlayerAnimationConstants.Jump, true);
        }

        public Type Tick()
        {
            if (PlayerStateData.IsShoot) 
            {
                _animator.SetBool(PlayerAnimationConstants.JumpThrow, true);
                return typeof(JumpProxyState);
            } 
            return typeof(EmptyState);
        }

        public Type FixedTick()
        {
            return PlayerStateData.IsOnGround ? typeof(JumpProxyState) : typeof(EmptyState);
        }

        public void Exit()
        {
            //_isShoot = false;
            //_player.OnShoot -= Shoot;
            if (PlayerStateData.IsShoot) PlayerStateData.IsShoot = false;
            _animator.SetBool(PlayerAnimationConstants.Jump, false);
        }
    }
}
