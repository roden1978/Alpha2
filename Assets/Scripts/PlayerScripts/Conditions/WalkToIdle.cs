using UnityEngine;

namespace PlayerScripts.Conditions
{
    public class WalkToIdle : ICondition
    {
        private readonly Rigidbody2D _rigidbody2D;
        private readonly float _dampingX;

        public WalkToIdle(Rigidbody2D rigidbody2D, float dampingX)
        {
            _rigidbody2D = rigidbody2D;
            _dampingX = dampingX;
        }

        public bool Examination()
        {
            return Mathf.Abs(_rigidbody2D.velocity.x) < _dampingX;
        }
    }
}