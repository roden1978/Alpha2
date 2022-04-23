using UnityEngine;

namespace PlayerScripts.Conditions
{
    public class JumpThrowToJump : ICondition
    {
        private readonly Animator _animator;

        public JumpThrowToJump(Animator animator)
        {
            _animator = animator;
        }

        public bool Examination()
        {
            return _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1;
        }
    }
}