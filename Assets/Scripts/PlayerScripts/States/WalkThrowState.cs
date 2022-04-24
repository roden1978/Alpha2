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
            _animator.Play(PlayerAnimationConstants.WalkThrowWithAxe);
            _footstepFx.Show();
        }

        public void Update() { }

        public void Exit() => _footstepFx.Hide();
    }
}
