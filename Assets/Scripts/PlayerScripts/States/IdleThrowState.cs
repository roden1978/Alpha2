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

        public override void Exit()
        {
            base.Exit();
        }

        public override Type Tick()
        {
            throw new NotImplementedException();
        }

        public override Type FixedTick()
        {
            throw new NotImplementedException();
        }
    }
}
