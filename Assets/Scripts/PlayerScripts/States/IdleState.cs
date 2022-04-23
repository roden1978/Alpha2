using Common;
using UnityEngine;

namespace PlayerScripts.States
{
    public class IdleState : IState
    {
        private bool _isShoot;
        public IdleState(bool isShoot) => 
            _isShoot = isShoot;

        public void Enter()
        {
            //Debug.Log("IdleState enter");
            if(_isShoot)
                _isShoot = false;
        }
        public void Update(){}
        public void Exit()
        {
            //Debug.Log("IdleState exit");
        }
    }
}