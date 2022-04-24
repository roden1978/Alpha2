using Common;
using UnityEngine;

namespace PlayerScripts.States
{
    public class JumpProxyState : IState
    {
        //private readonly Animator _animator;
        
        /*public JumpProxyState()
        {
           // _animator = animator;
        }*/

        public void Enter()
        {
            Debug.Log("JumpProxyState enter");
        }

        public void Update()
        {
            //if (_animator.GetBool(PlayerAnimationConstants.JumpThrow) &&
           //     !_animator.GetBool(PlayerAnimationConstants.Jump)) 
           //     return typeof(JumpThrowState);
            
            //return _animator.GetBool(PlayerAnimationConstants.Jump) == false ? 
           //     typeof(IdleState) : 
           //     typeof(EmptyState);
        }

        public void Exit()
        {
            Debug.Log("JumpProxyState exit");
        }
    }
}