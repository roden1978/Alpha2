using Common;
using Services.Input;
using UnityEngine;

namespace PlayerScripts.Conditions
{
    public class WalkToWalkThrow : ICondition
    {
        private bool _isShoot;
        private readonly Rigidbody2D _rigidbody2D;
        private readonly float _dampingX;

        public WalkToWalkThrow(IInputService inputService, Rigidbody2D rigidbody2D, float dampingX)
        {
            _rigidbody2D = rigidbody2D;
            _dampingX = dampingX;
            inputService.OnShoot += () => _isShoot = true;
            inputService.OnStopShoot += () => _isShoot = false;
        }

        public bool Result() => 
            Mathf.Abs(_rigidbody2D.velocity.x) > _dampingX && _isShoot;
    }
}