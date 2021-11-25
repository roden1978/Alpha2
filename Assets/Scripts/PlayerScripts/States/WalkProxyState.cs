using System;
using Common;
using UnityEngine;

namespace PlayerScripts.States
{
    public class WalkProxyState : IState
    {
        private readonly Animator _animator;
        
        public WalkProxyState(Animator animator)
        {
            _animator = animator;
        }

        public void Enter()
        {
        }

        public Type Tick()
        {
            if (_animator.GetBool(PlayerAnimationConstants.WalkThrow) &&
                !_animator.GetBool(PlayerAnimationConstants.Walk)) return typeof(WalkThrowState);
            
            return _animator.GetBool(PlayerAnimationConstants.Walk) == false ? 
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