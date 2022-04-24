using Common;
using UnityEngine;

namespace PlayerScripts.Conditions
{
    public class IdleThrowToIdle : ICondition
    {
        private readonly Animator _animator;

        public IdleThrowToIdle(Animator animator)
        {
            _animator = animator;
        }

        public bool Result()
        {
           // Debug.Log("IdleThrowWithAxe " + _animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            return _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && _animator.GetCurrentAnimatorStateInfo(0).IsTag("IdleThrowWithAxe");
        }
    }
}