using Common;
using UnityEngine;

namespace PlayerScripts.Conditions
{
    public class WalkToJump : ICondition
    {
        private readonly Rigidbody2D _rigidbody2D;
        private readonly IDipstick _dipstick;
        private readonly float _dampingY;

        public WalkToJump(Rigidbody2D rigidbody2D, IDipstick dipstick, float dampingY)
        {
            _rigidbody2D = rigidbody2D;
            _dipstick = dipstick;
            _dampingY = dampingY;
        }

        public bool Result() => 
            !_dipstick.Contact() && _rigidbody2D.velocity.y > _dampingY;
    }
}