using System;
using Common;
using UnityEngine;

namespace PlayerScripts.States
{
    public class IdleState : IState
    {
        private readonly Rigidbody2D _rigidbody;
        //private readonly IDipstick _dipstick;
        //private bool _isShoot;

        public IdleState(Rigidbody2D rigidbody2D)
        {
            _rigidbody = rigidbody2D;
        }

        public void Enter()
        {
            //_dipstick.OnShoot += Shoot;
        }

        public Type Tick()
        {
            if (Mathf.Abs(_rigidbody.velocity.x) > PlayerStateData.Damping.x)
                return typeof(WalkState);
            return PlayerStateData.IsShoot ? typeof(IdleThrowState) : typeof(EmptyState);
        }
       

       public Type FixedTick()
       {
           return !PlayerStateData.IsOnGround &&
               _rigidbody.velocity.y > PlayerStateData.Damping.y ? typeof(JumpState) : typeof(EmptyState);
       }

       public void Exit()
       {
           if (PlayerStateData.IsShoot) PlayerStateData.IsShoot = false;
           //_dipstick.OnShoot -= Shoot;
       }
       
    }
}
