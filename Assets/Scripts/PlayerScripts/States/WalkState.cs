using System;
using Common;
using UnityEngine;

namespace PlayerScripts.States
{
    public class WalkState : BaseState
    {
        private Animator _animator;
        public WalkState(GameObject gameObject) : base(gameObject)
        {
            GameObject = gameObject;
            _animator.GetComponentInChildren<Animator>();
        }

        protected override void Enter()
        {
            _animator.SetBool(0, true);
        }

        public override Type Tick()
        {
            throw new NotImplementedException();
        }

        public override Type FixedTick()
        {
            throw new NotImplementedException();
        }

        protected override void Exit()
        {
            throw new NotImplementedException();
        }
    }
}