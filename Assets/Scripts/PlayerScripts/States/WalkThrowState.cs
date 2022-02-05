using System;
using Common;
using UnityEngine;

namespace PlayerScripts.States
{
    public class WalkThrowState : IState
    {
        private readonly Animator _animator;

        public WalkThrowState(Animator animator)
        {
            _animator = animator;
        }

        public void Enter()
        {
        }

        public Type Update()
        {
            return _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 ? 
                typeof(WalkState) : 
                typeof(EmptyState);
        }

        public void Exit()
        {
            _animator.SetBool(PlayerAnimationConstants.WalkThrow, false);
        }
    }
}
