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
        private readonly PlayerView _playerView;
        private static readonly int Walk = Animator.StringToHash("Walk");

        public WalkState(GameObject player) : base(player)
        {
            _rigidbody = player.GetComponent<Rigidbody2D>();
            _playerView = player.GetComponentInChildren<PlayerView>();
            if (_playerView.TryGetComponent(out Animator animator)) _animator = animator;
            _player = player.GetComponent<Player>();
        }

        public override void Enter()
        {
            _animator.SetBool(Walk, true);
        }

        public override Type Tick()
        {
            return Mathf.Abs(_rigidbody.velocity.x) < _player.XMoveDamping 
                ? typeof(IdleState) 
                : typeof(EmptyState);
        }

        public override Type FixedTick()
        {
            return !_player.StayOnGround() ? typeof(JumpState) : typeof(EmptyState);
        }
        

        public override void Exit()
        {
            _animator.SetBool(Walk, false);
        }
    }
}