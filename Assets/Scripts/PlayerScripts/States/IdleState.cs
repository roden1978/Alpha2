using System;
using Common;
using UnityEngine;

namespace PlayerScripts.States
{
    public class IdleState : BaseState
    {
        private readonly Rigidbody2D _rigidbody;
        public IdleState(GameObject gameObject) : base(gameObject)
        {
            GameObject = gameObject;
            _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        }

       public override Type Tick() => Mathf.Abs(_rigidbody.velocity.x) > 0.3f 
           ? typeof(WalkState) 
           : typeof(EmptyState);
       

       public override Type FixedTick() => typeof(EmptyState);

       public override void Exit(){}
    }
}
