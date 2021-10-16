using System;
using Common;
using UnityEngine;

namespace PlayerScripts.States
{
    public class IdleThrowState : BaseState
    {

        private readonly Animator _animator;
        private readonly int _idleThrow = Animator.StringToHash("IdleThrow");

        public IdleThrowState(GameObject player) : base(player)
        {
            _animator = player.GetComponentInChildren<Animator>();
        }

        public override void Enter()
        {
            _animator.SetTrigger(_idleThrow);
        }

        public override Type Tick()
        {
            return _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 ? 
                typeof(IdleState) : 
                typeof(EmptyState);
        }

        public override Type FixedTick()
        {
            return typeof(EmptyState);
        }

        public override void Exit()
        {
            _animator.SetTrigger(_idleThrow);
        }
    }
}
