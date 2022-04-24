using Common;
using UnityEngine;

namespace PlayerScripts.States
{
    public class IdleState : IState
    {
        public void Enter()
        {
            Debug.Log("IdleState enter");
        }
        public void Update(){}
        public void Exit()
        {
            Debug.Log("IdleState exit");
        }
    }
}