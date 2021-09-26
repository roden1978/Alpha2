using System;
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

        public WalkState(GameObject gameObject, DevicesInput devicesInput) 
            : base(gameObject)
        {
            GameObject = gameObject;
            _devicesInput = devicesInput;
            _animator = gameObject.GetComponentInChildren<Animator>();
        }

        public override void Enter()
        {
            _animator.SetBool(Walk, true);
        }

        public override Type Tick()
        {
            return Mathf.Abs(_devicesInput.Direction) == 0 ? typeof(IdleState) : typeof(EmptyState);
        }

        public override Type FixedTick()
        {
            return typeof(EmptyState);
        }

        public override void Exit()
        {
            _animator.SetBool(Walk, false);
        }
    }
}