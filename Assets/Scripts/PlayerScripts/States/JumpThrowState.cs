using Common;
using UnityEngine;

namespace PlayerScripts.States
{
    public class JumpThrowState : IState
    {
        private readonly Animator _animator;

        public JumpThrowState(Animator animator)
        {
            _animator = animator;
        }

        public void Enter()
        {
            _animator.Play(PlayerAnimationConstants.JumpThrowWithAxe);
        }

        public void Update() { }

        public void Exit() { }
    }
}
