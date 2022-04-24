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
            Debug.Log("JumpThrowState enter");
            _animator.SetBool(PlayerAnimationConstants.JumpThrow, true);
        }

        public void Update()
        {
            //return _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 ? 
            //    typeof(JumpState) : 
            //    typeof(EmptyState);
        }

        public void Exit()
        {
            Debug.Log("JumpThrowState exit");
            _animator.SetBool(PlayerAnimationConstants.JumpThrow, false);
        }
    }
}
