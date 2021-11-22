using System;
using Common;
using UnityEngine;

namespace PlayerScripts.States
{
    public class IdleState : IState
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly Player _player;
        private bool _isShoot;

        public IdleState(Player player)
        {
            _player = player;
            _rigidbody = player.GetComponent<Rigidbody2D>();
        }

        public void Enter()
        {
            _player.OnShoot += Shoot;
        }

        public Type Tick()
        {
            if (Mathf.Abs(_rigidbody.velocity.x) > _player.XMoveDamping)
                return typeof(WalkState);
            return _isShoot ? typeof(IdleThrowState) : typeof(EmptyState);
        }
       

       public Type FixedTick()
       {
           return !_player.StayOnGround() &&
               _rigidbody.velocity.y > _player.YMoveDamping ? typeof(JumpState) : typeof(EmptyState);
       }

       public void Exit()
       {
           _isShoot = false;
           _player.OnShoot -= Shoot;
       }

       private void Shoot()
       {
           _isShoot = true;
       }
    }
}
