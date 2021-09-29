using System;
using Common;
using UnityEngine;

namespace PlayerScripts.States
{
    public class JumpState : BaseState
    {
        private readonly Animator _animator;
        private readonly Player _player;
        private static readonly int Jump = Animator.StringToHash("Jump");
        
        public JumpState(GameObject player) : base(player)
        {
            player.TryGetComponent(out _player);
            var playerView = player.GetComponentInChildren<PlayerView>();
            if (playerView.TryGetComponent(out Animator animator)) _animator = animator;

        }

        public override void Enter()
        {
            _animator.SetBool(Jump, true);
        }

        public override Type Tick()
        {
            return typeof(EmptyState);
        }

        public override Type FixedTick()
        {
            return _player.StayOnGround() ? typeof(IdleState) : typeof(EmptyState);
        }

        public override void Exit()
        {
            _animator.SetBool(Jump, false);
        }
    }
}
