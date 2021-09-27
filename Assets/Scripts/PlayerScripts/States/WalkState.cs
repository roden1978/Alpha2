using System;
using Common;
using UnityEngine;

namespace PlayerScripts.States
{
    
    public class WalkState : BaseState
    {
        private readonly Animator _animator;
        private readonly Rigidbody2D _rigidbody;
        private readonly Player _player;
        private readonly PlayerSurfaceNormal _playerSurfaceNormal;
        private static readonly int Walk = Animator.StringToHash("Walk");

        public WalkState(GameObject gameObject) 
            : base(gameObject)
        {
            GameObject = gameObject;
            _rigidbody = gameObject.GetComponent<Rigidbody2D>();
            _animator = gameObject.GetComponentInChildren<Animator>();
            _player = gameObject.GetComponent<Player>();
            _playerSurfaceNormal = new PlayerSurfaceNormal(_player);
        }

        public override void Enter()
        {
            _animator.SetBool(Walk, true);
        }

        public override Type Tick()
        {
            if (Mathf.Abs(_rigidbody.velocity.x) < _player.XMoveDamping)
                return typeof(IdleState);

            return _playerSurfaceNormal.Value() == Vector3.zero ? typeof(JumpState) : typeof(EmptyState);
        }

        public override Type FixedTick() => typeof(EmptyState);
        

        public override void Exit()
        {
            _animator.SetBool(Walk, false);
        }
    }
}