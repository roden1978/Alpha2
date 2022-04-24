using Common;
using UnityEngine;

namespace PlayerScripts.States
{
    public class IdleState : IState
    {
        private readonly Animator _animator;

        public IdleState(Animator animator)
        {
            _animator = animator;
        }

        public void Enter()
        {
            if(!_animator.GetCurrentAnimatorStateInfo(0).IsName("IdleWithAxe"))
                _animator.Play(PlayerAnimationConstants.IdleWithAxe);
        }
        public void Update(){}
        public void Exit() { }
    }
}