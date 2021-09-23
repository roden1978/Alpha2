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
            Debug.Log($"Abs: {Mathf.Abs(_prevPosition.x - GameObject.transform.position.x)}");
            /*if (Mathf.Abs(_prevPosition.x - GameObject.transform.position.x) == 0)
                StateMachine.PushState(typeof(IdleState));*/
            
            _prevPosition.x = GameObject.transform.position.x;
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