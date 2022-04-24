using Common;
using UnityEngine;

namespace PlayerScripts.Conditions
{
    public class JumpThrowToJump : ICondition
    {
        private readonly Animator _animator;

        public JumpThrowToJump(Animator animator) => 
            _animator = animator;

        public bool Result() => 
            _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1;
    }
}
