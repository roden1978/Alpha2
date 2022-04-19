using UnityEngine;

namespace PlayerScripts.Conditions
{
    public class IdleToJump : ICondition
    {
        private readonly Rigidbody2D _rigidbody2D;
        private readonly bool _isOnGround;
        private readonly float _dampingY;
        
        public IdleToJump(Rigidbody2D rigidbody2D, bool isOnGround, float dampingY)
        {
            _rigidbody2D = rigidbody2D;
            _isOnGround = isOnGround;
            _dampingY = dampingY;
        }
        public bool Examination() => 
            !_isOnGround && _rigidbody2D.velocity.y > _dampingY;
    }
}