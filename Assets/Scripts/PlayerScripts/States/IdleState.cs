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
            Debug.Log("IdleState enter");
            if(!_animator.GetCurrentAnimatorStateInfo(0).IsName("IdleWithAxe"))
                _animator.Play("IdleWithAxe");
        }
        public void Update(){}
        public void Exit()
        {
            Debug.Log("IdleState exit");
        }
    }
}