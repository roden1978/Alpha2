using System;
using Common;
using UnityEngine;

namespace PlayerScripts.States
{
    public class JumpThrowState : IState
    {
        private readonly Animator _animator;

        public JumpThrowState(Animator animator)
        {
            _animator = animator;
        }

        public void Enter()
        {
        }

        public Type Tick()
        {
            return _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 ? 
                typeof(JumpState) : 
                typeof(EmptyState);
        }

        public Type FixedTick()
        {
            return typeof(EmptyState);
        }

        public void Exit()
        {
            _animator.SetBool(PlayerAnimationConstants.JumpThrow, false);
        }
    }
}
