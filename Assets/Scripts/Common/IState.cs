using System;
using UnityEngine;

namespace Common
{
    public interface IState
    {
        public void Enter();
        public Type Update();
        public void Exit();
    }
    
}