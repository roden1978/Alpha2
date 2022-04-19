using Common;

namespace PlayerScripts.States
{
    public class IdleState : IState
    {
        private bool _isShoot;
        public IdleState(bool isShoot) => 
            _isShoot = isShoot;
        public void Enter(){}
        public void Tick(){}
        public void Exit()
        {
            if(_isShoot)
                _isShoot = false;
        }
    }
}