using Common;

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
        }
    }
}