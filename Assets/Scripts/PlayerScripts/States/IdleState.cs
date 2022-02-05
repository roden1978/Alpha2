using System;
using Common;
using UnityEngine;

namespace PlayerScripts.States
{
    public class IdleState : IState
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly PlayerStateData _playerStateData;
        public IdleState(Rigidbody2D rigidbody2D, PlayerStateData playerStateData)
        {
            _rigidbody = rigidbody2D;
            _playerStateData = playerStateData;
        }

        public void Enter()
        {
        }

        public Type Update()
        {
            if (Mathf.Abs(_rigidbody.velocity.x) > _playerStateData.Damping.x)
                return typeof(WalkState);
            if(!_playerStateData.IsOnGround && _rigidbody.velocity.y > _playerStateData.Damping.y)
                return typeof(JumpState);
            return _playerStateData.IsShoot ? typeof(IdleThrowState) : typeof(EmptyState);
        }

        public void Exit()
       {
           if (_playerStateData.IsShoot) _playerStateData.IsShoot = false;
       }
       
    }
}
