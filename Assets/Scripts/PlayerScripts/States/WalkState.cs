using Common;
using GameObjectsScripts;
using UnityEngine;

namespace PlayerScripts.States
{
    
    public class WalkState : IState
    {
        private readonly Animator _animator;
        private bool _isShoot;
        private readonly IShowable _footstepFx;

        public WalkState(Animator animator, bool isShoot,
            IShowable footstepFx)
        {
            _animator = animator;
            _isShoot = isShoot;
            _footstepFx = footstepFx;
        }

        public void Enter()
        {
            Debug.Log("Walk state enter");
            _animator.Play("WalkWithAxe");
            _footstepFx.Show();
        }

       
        public void Update()
        {
         
        }

        public void Exit()
        {
            Debug.Log("Walk state exit");
            _footstepFx.Hide();
        }
    }
}