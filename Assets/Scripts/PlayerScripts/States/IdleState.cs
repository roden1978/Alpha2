using System;
using Common;
using UnityEngine;

namespace PlayerScripts.States
{
    public class IdleState : BaseState
    {
        public IdleState(GameObject gameObject) : base(gameObject)
        {
            GameObject = gameObject;
        }

        protected override void Enter()
        {
            
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
