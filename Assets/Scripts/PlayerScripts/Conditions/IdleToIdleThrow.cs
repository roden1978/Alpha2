using Common;
using Services.Input;
using UnityEngine;

namespace PlayerScripts.Conditions
{
    public class IdleToIdleThrow : ICondition
    {
        private bool _isShoot;
        private readonly IInputService _inputService;
        private readonly Rigidbody2D _rigidbody2D;
        private readonly float _dampingX;

        public IdleToIdleThrow(IInputService inputService, Rigidbody2D rigidbody2D, float dampingX)
        {
            _inputService = inputService;
            _rigidbody2D = rigidbody2D;
            _dampingX = dampingX;
            inputService.OnShoot += () => _isShoot = true;
            inputService.OnStopShoot += () => _isShoot = false;
        }

        public bool Result() => 
            Mathf.Abs(_rigidbody2D.velocity.x * _inputService.Move()) < _dampingX && _isShoot;
    }
}