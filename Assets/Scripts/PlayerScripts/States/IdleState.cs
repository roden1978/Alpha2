using System;
using Common;
using UnityEngine;

namespace PlayerScripts.States
{
    public class IdleState : BaseState
    {
        private Vector3 _prevPosition;
        public IdleState(GameObject gameObject, StateMachine stateMachine) : base(gameObject, stateMachine)
        {
            GameObject = gameObject;
            StateMachine = stateMachine;
        }

       public override void Tick()
        {
            if((int)(_prevPosition - GameObject.transform.position).magnitude != 0)
                StateMachine.PushState(typeof(WalkState));
            _prevPosition = GameObject.transform.position;
            
        }

       public override void FixedTick()
       {
           
       }

       public override void Exit()
        {
            StateMachine.PopState();
        }
    }
}
