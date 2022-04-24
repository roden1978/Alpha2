using Common;
using GameObjectsScripts;
using UnityEngine;

namespace PlayerScripts.States
{
    public class WalkThrowState : IState
    {
        private readonly Animator _animator;
        private readonly IShowable _footstepFx;

        public WalkThrowState(Animator animator, IShowable footstepFx)
        {
            _animator = animator;
            _footstepFx = footstepFx;
        }

        public void Enter()
        {
            Debug.Log("Walk throw state enter");
            _animator.Play("WalkThrowWithAxe");
            _footstepFx.Show();
        }

        public void Update()
        {
            //return _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 ? 
            //    typeof(WalkState) : 
            //    typeof(EmptyState);
        }

        public void Exit()
        {
            Debug.Log("Walk throw state exit");
            _footstepFx.Hide();
        }
    }
}
