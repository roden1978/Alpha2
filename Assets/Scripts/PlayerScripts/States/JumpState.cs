using Common;
using GameObjectsScripts;
using UnityEngine;

namespace PlayerScripts.States
{
    public class JumpState : IState
    {
        private readonly Animator _animator;
        private readonly IShowable _groundingFx;
        private readonly IShowable _jumpFx;
        private readonly IDipstick _dipstick;
        public JumpState(Animator animator, IDipstick dipstick, IShowable groundingFx, IShowable jumpFx)
        {
            _dipstick = dipstick;
            _animator = animator;
            _groundingFx = groundingFx;
            _jumpFx = jumpFx;
        }

        public void Enter()
        {
            _animator.Play(PlayerAnimationConstants.JumpWithAxe);
            _jumpFx.Show();
        }

        public void Update(){}

        public void Exit()
        {
            if(_dipstick.Contact())
                _groundingFx.Show();
            _jumpFx.Hide();
        }
    }
}
