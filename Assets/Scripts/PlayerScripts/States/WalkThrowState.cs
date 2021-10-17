using System;
using Common;
using UnityEngine;

namespace PlayerScripts.States
{
    public class WalkThrowState : BaseState
    {
        private readonly Animator _animator;

        public WalkThrowState(GameObject player) : base(player)
        {
            _animator = player.GetComponentInChildren<Animator>();
        }

        public override Type Tick()
        {
            return _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 ? 
                typeof(WalkState) : 
                typeof(EmptyState);
        }

        public override Type FixedTick()
        {
            return typeof(EmptyState);
        }

        public override void Exit()
        {
            _animator.SetBool(WalkThrow, false);
        }
    }
}
