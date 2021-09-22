using System;
using Common;
using UnityEngine;

namespace PlayerScripts.States
{
    public class WalkState : BaseState
    {
        private readonly Animator _animator;
        private Vector3 _prevPosition;
        private static readonly int Walk = Animator.StringToHash("Walk");

        public WalkState(GameObject gameObject, StateMachine stateMachine) 
            : base(gameObject, stateMachine)
        {
            GameObject = gameObject;
            StateMachine = stateMachine;
            _animator = gameObject.GetComponentInChildren<Animator>();
        }

        public override void Enter()
        {
            _animator.SetBool(Walk, true);
        }

        public override void Tick()
        {
            if((_prevPosition - GameObject.transform.position).magnitude == 0)
                StateMachine.PushState(typeof(WalkState));
            _prevPosition = GameObject.transform.position;
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