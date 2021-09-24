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
            _prevPosition = gameObject.transform.position;
        }

       public override void Tick()
        {
            if(Mathf.Abs(_prevPosition.x - GameObject.transform.position.x) > 0)
                StateMachine.PushState(typeof(WalkState));
            
            _prevPosition.x = GameObject.transform.position.x;
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
