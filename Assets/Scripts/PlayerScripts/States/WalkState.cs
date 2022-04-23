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
            _animator.SetBool(PlayerAnimationConstants.Walk, true);
            _footstepFx.Show();
        }

       
        public void Update()
        {
            //if (_playerStateData.IsShoot) 
            //{
           //     _animator.SetBool(PlayerAnimationConstants.WalkThrow, true);
           //     return typeof(WalkProxyState);
           // }

            //if (!_playerStateData.IsOnGround && _rigidbody.velocity.y > _playerStateData.Damping.y)
            //    return typeof(JumpState);
            
            //return Mathf.Abs(_rigidbody.velocity.x) < _playerStateData.Damping.x ? typeof(IdleState) : typeof(EmptyState);
        }

        public void Exit()
        {
            if (_isShoot)
            {
                _animator.SetBool(PlayerAnimationConstants.WalkThrow, true);
                _isShoot = false;
            }
            _animator.SetBool(PlayerAnimationConstants.Walk, false);
            _footstepFx.Hide();
        }
    }
}