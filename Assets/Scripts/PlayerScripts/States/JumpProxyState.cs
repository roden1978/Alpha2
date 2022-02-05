using System;
using Common;
using UnityEngine;

namespace PlayerScripts.States
{
    public class JumpProxyState : IState
    {
        private readonly Animator _animator;
        
        public JumpProxyState(Animator animator)
        {
            _animator = animator;
        }

        public void Enter()
        {
        }

        public Type Update()
        {
            if (_animator.GetBool(PlayerAnimationConstants.JumpThrow) &&
                !_animator.GetBool(PlayerAnimationConstants.Jump)) 
                return typeof(JumpThrowState);
            
            return _animator.GetBool(PlayerAnimationConstants.Jump) == false ? 
                typeof(IdleState) : 
                typeof(EmptyState);
        }

        public void Exit()
        {
        }
    }
}