using System;
using Common;
using UnityEngine;

namespace PlayerScripts.States
{
    public class IdleState : BaseState
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly Player _player;
        public IdleState(GameObject gameObject) : base(gameObject)
        {
            GameObject = gameObject;
            _rigidbody = gameObject.GetComponent<Rigidbody2D>();
            _player = gameObject.GetComponent<Player>();
        }

       public override Type Tick()
       {
           if(Mathf.Abs(_rigidbody.velocity.x) > _player.XMoveDamping)
               return typeof(WalkState);
           
           return typeof(EmptyState);
       }
       

       public override Type FixedTick()
       {
           return !_player.StayOnGround() ? typeof(JumpState) : typeof(EmptyState);
       } 

       public override void Exit(){}
    }
}
