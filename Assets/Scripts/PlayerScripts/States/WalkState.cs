using System;
using Common;
using UnityEngine;

namespace PlayerScripts.States
{
    
    public class WalkState : BaseState
    {
        private readonly Animator _animator;
        private readonly Rigidbody2D _rigidbody;
        private static readonly int Walk = Animator.StringToHash("Walk");

        public WalkState(GameObject gameObject) 
            : base(gameObject)
        {
            GameObject = gameObject;
            _rigidbody = gameObject.GetComponent<Rigidbody2D>();
            _animator = gameObject.GetComponentInChildren<Animator>();
        }

        public override void Enter()
        {
            _animator.SetBool(Walk, true);
        }

        public override Type Tick() => Mathf.Abs(_rigidbody.velocity.x) < 0.1f 
            ? typeof(IdleState) 
            : typeof(EmptyState);

        public override Type FixedTick() => typeof(EmptyState);
        

        public override void Exit()
        {
            _animator.SetBool(Walk, false);
        }
    }
}