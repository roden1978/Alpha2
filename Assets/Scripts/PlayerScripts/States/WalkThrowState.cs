using System;
using Common;
using UnityEngine;

namespace PlayerScripts.States
{
    public class WalkThrowState : IState
    {
        private readonly Animator _animator;

        public WalkThrowState(Component player)
        {
            _animator = player.GetComponentInChildren<Animator>();
        }

        public void Enter()
        {
        }

        public Type Tick()
        {
            return _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 ? 
                typeof(WalkState) : 
                typeof(EmptyState);
        }

        public Type FixedTick()
        {
            return typeof(EmptyState);
        }

        public void Exit()
        {
            _animator.SetBool(PlayerAnimationConstants.WalkThrow, false);
        }
    }
}
