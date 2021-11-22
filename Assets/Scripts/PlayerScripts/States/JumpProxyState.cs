using System;
using Common;
using UnityEngine;

namespace PlayerScripts.States
{
    public class JumpProxyState : IState
    {
        private readonly Animator _animator;
        
        public JumpProxyState(Component player)
        {
            _animator = player.GetComponentInChildren<Animator>();
        }

        public void Enter()
        {
        }

        public Type Tick()
        {
            if (_animator.GetBool(PlayerAnimationConstants.JumpThrow) &&
                !_animator.GetBool(PlayerAnimationConstants.Jump)) 
                return typeof(JumpThrowState);
            
            return _animator.GetBool(PlayerAnimationConstants.Jump) == false ? 
                typeof(IdleState) : 
                typeof(EmptyState);
        }

        public Type FixedTick()
        {
            return typeof(EmptyState);
        }

        public void Exit()
        {
        }
    }
}