using Common;
using UnityEngine;

namespace PlayerScripts.Conditions
{
    public class WalkProxyToIdle : ICondition
    {
        private readonly Animator _animator;

        public WalkProxyToIdle(Animator animator)
        {
            _animator = animator;
        }

        public bool Result()
        {
            return _animator.GetBool(PlayerAnimationConstants.Walk) == false;
        }
    }
}