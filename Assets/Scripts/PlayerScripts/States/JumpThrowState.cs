using System;
using Common;
using UnityEngine;

namespace PlayerScripts.States
{
    public class JumpThrowState : BaseState
    {
        private readonly Animator _animator;

        public JumpThrowState(GameObject player) : base(player)
        {
            _animator = player.GetComponentInChildren<Animator>();
        }

        public override void Enter()
        {
            _animator.SetBool(JumpThrow, true);
        }

        public override Type Tick()
        {
            return _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 ? 
                typeof(JumpState) : 
                typeof(EmptyState);
        }

        public override Type FixedTick()
        {
            return typeof(EmptyState);
        }

        public override void Exit()
        {
            _animator.SetBool(JumpThrow, false);
        }
    }
}
