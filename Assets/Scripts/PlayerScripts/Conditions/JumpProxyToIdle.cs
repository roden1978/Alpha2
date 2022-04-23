using Common;
using UnityEngine;

namespace PlayerScripts.Conditions
{
    public class JumpProxyToIdle : ICondition
    {
        private readonly Animator _animator;

        public JumpProxyToIdle(Animator animator)
        {
            _animator = animator;
        }

        public bool Result()
        {
            Debug.Log("JumpToIdle");
            return !_animator.GetBool(PlayerAnimationConstants.Jump);
        }
    }
}