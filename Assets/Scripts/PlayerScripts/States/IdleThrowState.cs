using System;
using Common;
using UnityEngine;

namespace PlayerScripts.States
{
    public class IdleThrowState : IState
    {
        private readonly Animator _animator;

        public IdleThrowState(Animator animator)
        {
            _animator = animator;
        }

        public void Enter()
        {
            _animator.SetBool(PlayerAnimationConstants.IdleThrow, true);
        }

        public Type Tick()
        {
            return _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 ? 
                typeof(IdleState) : 
                typeof(EmptyState);
        }

        public Type FixedTick()
        {
            return typeof(EmptyState);
        }

        public void Exit()
        {
            _animator.SetBool(PlayerAnimationConstants.IdleThrow, false);
        }
    }
}
