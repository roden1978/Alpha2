using System;
using Common;
using UnityEngine;

namespace PlayerScripts.States
{
    public class JumpState : BaseState
    {
        private readonly Animator _animator;
        private readonly Player _player;
        private readonly PlayerSurfaceNormal _playerSurfaceNormal;
        private static readonly int Jump = Animator.StringToHash("Jump");
        
        public JumpState(GameObject gameObject) : base(gameObject)
        {
            GameObject = gameObject;
            _animator = gameObject.GetComponentInChildren<Animator>();
            _player = gameObject.GetComponent<Player>();
            //_rigidbody = gameObject.GetComponent<Rigidbody2D>();
            _playerSurfaceNormal = new PlayerSurfaceNormal(_player);
        }

        public override void Enter()
        {
            _animator.SetBool(Jump, true);
        }

        public override Type Tick()
        {
            return _playerSurfaceNormal.Value() != Vector3.zero ? typeof(JumpState) : typeof(EmptyState);
        }

        public override Type FixedTick()
        {
            return typeof(EmptyState);
        }

        public override void Exit()
        {
            _animator.SetBool(Jump, false);
        }
    }
}
