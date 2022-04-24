using Common;
using UnityEngine;

namespace PlayerScripts.Conditions
{
    public class IdleToWalk : ICondition
    {
        private readonly Rigidbody2D _rigidbody2D;
        private readonly float _dampingX;

        public IdleToWalk(Rigidbody2D rigidbody2D, float dampingX)
        {
            _rigidbody2D = rigidbody2D;
            _dampingX = dampingX;
        }

        public bool Result() => 
            Mathf.Abs(_rigidbody2D.velocity.x) > _dampingX;
    }
}