using Common;
using UnityEngine;

namespace PlayerScripts.Conditions
{
    public class JumpProxyToJumpThrow : ICondition
    {
        private readonly Animator _animator;
        public JumpProxyToJumpThrow(Animator animator)
        {
            _animator = animator;
        }

        public bool Examination()
        {
            return JumpThrow() && Jump();
        }

        private bool Jump()
        {
            return !_animator.GetBool(PlayerAnimationConstants.Jump);
        }

        private bool JumpThrow()
        {
            return _animator.GetBool(PlayerAnimationConstants.JumpThrow);
        }
    }
}