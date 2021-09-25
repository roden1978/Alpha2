using Common;
using Input;
using UnityEngine;

namespace PlayerScripts.States
{
    public class WalkState : BaseState
    {
        private readonly Animator _animator;
        private readonly DevicesInput _devicesInput;
        private static readonly int Walk = Animator.StringToHash("Walk");

        public WalkState(GameObject gameObject, StateMachine stateMachine, DevicesInput devicesInput) 
            : base(gameObject, stateMachine)
        {
            GameObject = gameObject;
            StateMachine = stateMachine;
            _devicesInput = devicesInput;
            _animator = gameObject.GetComponentInChildren<Animator>();
        }

        public override void Enter()
        {
            _animator.SetBool(Walk, true);
        }

        public override void Tick()
        {
            if (Mathf.Abs(_devicesInput.Direction) == 0)
                StateMachine.PushState(typeof(IdleState));
        }

        public override void FixedTick()
        {
                
        }

        public override void Exit()
        {
            _animator.SetBool(Walk, false);
            StateMachine.PopState();
        }
    }
}