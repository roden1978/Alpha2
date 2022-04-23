using Common;
using UnityEngine;

namespace PlayerScripts.Conditions
{
    public class WalkProxyToWalkThrow : ICondition
    {
        private readonly Animator _animator;

        public WalkProxyToWalkThrow(Animator animator)
        {
            _animator = animator;
        }

        public bool Result()
        {
            return _animator.GetBool(PlayerAnimationConstants.WalkThrow) &&
                   !_animator.GetBool(PlayerAnimationConstants.Walk);
        }
    }
}