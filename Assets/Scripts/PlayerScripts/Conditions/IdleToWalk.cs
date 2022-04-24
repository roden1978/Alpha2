using Common;
using Services.Input;
using UnityEngine;

namespace PlayerScripts.Conditions
{
    public class IdleToWalk : ICondition
    {
        private readonly Rigidbody2D _rigidbody2D;
        private readonly float _dampingX;
        private bool _isShoot;

        public IdleToWalk(Rigidbody2D rigidbody2D, float dampingX, IInputService inputService)
        {
            _rigidbody2D = rigidbody2D;
            _dampingX = dampingX;
            //inputService.OnShoot += () => _isShoot = true;
            //inputService.OnStopShoot += () => _isShoot = false;
        }

        public bool Result()
        {
            return Mathf.Abs(_rigidbody2D.velocity.x) > _dampingX;
        }
    }
}