using System;
using UnityEngine;

namespace Common
{
    public interface IState
    {
        public void Enter();
        public Type Tick();
        public Type FixedTick();
        public void Exit();
    }
    
}