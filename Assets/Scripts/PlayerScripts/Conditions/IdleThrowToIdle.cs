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

        public bool Examination()
        {
            return _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1;
        }
    }
}