using System;
using Common;
using UnityEngine;
using UnityEngine.Experimental.AI;

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
            Debug.Log(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
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
            Debug.Log("Exit Throw");
            _animator.SetTrigger(_idleThrow);
        }
    }
}
