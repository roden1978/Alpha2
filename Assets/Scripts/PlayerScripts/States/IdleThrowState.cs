using Common;
using UnityEngine;

namespace PlayerScripts.States
{
    public class IdleThrowState : IState
    {
        private readonly Animator _animator;

        public IdleThrowState(Animator animator)
        {
            _animator = animator;
        }

        public void Enter()
        {
            _animator.Play(PlayerAnimationConstants.IdleThrowWithAxe);
        }

        public void Update(){}

        public void Exit() { }
    }
}
