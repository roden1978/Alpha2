using Common;
using Services.Input;
using UnityEngine;

namespace PlayerScripts.Conditions
{
    public class IdleToWalk : ICondition
    {
        private readonly Rigidbody2D _rigidbody2D;
        private readonly float _dampingX;
        private readonly IInputService _inputService;

        public IdleToWalk(Rigidbody2D rigidbody2D, float dampingX, IInputService inputService)
        {
            _rigidbody2D = rigidbody2D;
            _dampingX = dampingX;
            _inputService = inputService;
        }

        public bool Result() => 
            Mathf.Abs(_rigidbody2D.velocity.x * _inputService.Move()) > _dampingX;
    }
}