using System;
using Common;
using UnityEngine;

namespace PlayerScripts.States
{
    public class IdleState : BaseState
    {
        private Vector3 _prevPosition;
        public IdleState(GameObject gameObject) : base(gameObject)
        {
            GameObject = gameObject;
            _prevPosition = gameObject.transform.position;
        }

       public override Type Tick()
        {
            if(Mathf.Abs(_prevPosition.x - GameObject.transform.position.x) > 0)
                return typeof(WalkState);
            
            _prevPosition.x = GameObject.transform.position.x;
            return typeof(EmptyState);
        }

       public override Type FixedTick()
       {
           return typeof(EmptyState);
       }

       public override void Exit()
        {
            
        }
    }
}
