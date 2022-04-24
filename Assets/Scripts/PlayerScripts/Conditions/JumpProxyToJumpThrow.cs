using Common;
using Services.Input;
using UnityEngine;

namespace PlayerScripts.Conditions
{
    public class JumpProxyToJumpThrow : ICondition
    {
        private readonly Animator _animator;
        private bool _isShoot;
        public JumpProxyToJumpThrow(Animator animator, IInputService inputService)
        {
            _animator = animator;
            inputService.OnShoot += () => _isShoot = true;
            inputService.OnStopShoot += () => _isShoot = false;
        }

        public bool Result()
        {
            return JumpThrow() && _isShoot;
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