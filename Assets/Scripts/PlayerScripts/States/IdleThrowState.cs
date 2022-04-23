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
            //Debug.Log("IdleThrowState enter");
            _animator.SetBool(PlayerAnimationConstants.IdleThrow, true);
        }

        public void Update(){}

        public void Exit()
        {
            //Debug.Log("IdleThrowState exit");
            _animator.SetBool(PlayerAnimationConstants.IdleThrow, false);
        }
    }
}
