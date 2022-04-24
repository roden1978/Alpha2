using Common;
using UnityEngine;

namespace PlayerScripts.Conditions
{
    public class JumpThrowToJump : ICondition
    {
        private readonly Animator _animator;

        public JumpThrowToJump(Animator animator)
        {
            _animator = animator;
            //Debug.Log("JumpThrowToJump");
        }

        public bool Result()
        {
            //Debug.Log("JumpThrowWithAxe " + _animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            return _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && _animator.GetCurrentAnimatorStateInfo(0).IsTag("JumpThrowWithAxe");
        }
    }
}