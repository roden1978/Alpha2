using System;
using Common;
using UnityEngine;

namespace PlayerScripts.States
{
    public class IdleState : BaseState
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly Player _player;
        public IdleState(GameObject player) : base(player)
        {
            _rigidbody = player.GetComponent<Rigidbody2D>();
            _player = player.GetComponent<Player>();
        }

       public override Type Tick()
       {
           if(Mathf.Abs(_rigidbody.velocity.x) > _player.XMoveDamping)
               return typeof(WalkState);
           
           return typeof(EmptyState);
       }
       

       public override Type FixedTick()
       {
           return !_player.StayOnGround() &&
               _rigidbody.velocity.y > _player.YMoveDamping? typeof(JumpState) : typeof(EmptyState);
       } 

       public override void Exit(){}
    }
}
