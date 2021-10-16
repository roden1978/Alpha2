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
        private bool _isShoot;

        public JumpState(GameObject player) : base(player)
        {
            _player = player.TryGetComponent(out _player) ? player.GetComponent<Player>() : null;
            _animator = player.GetComponentInChildren<Animator>();
        }

        public override void Enter()
        {
            _player.OnShoot += Shoot;
            _animator.SetBool(Jump, true);
        }

        private void Shoot()
        {
            _isShoot = true;
        }

        public override Type Tick()
        {
            return _isShoot ? typeof(IdleThrowState) : typeof(EmptyState);
        }

        public override Type FixedTick()
        {
            return _player.StayOnGround() ? typeof(IdleState) : typeof(EmptyState);
        }

        public override void Exit()
        {
            _isShoot = false;
            _player.OnShoot -= Shoot;
            _animator.SetBool(Jump, false);
        }
    }
}
