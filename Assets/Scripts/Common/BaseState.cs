using System;
using UnityEngine;

namespace Common
{
    public abstract class BaseState
    {
        protected GameObject GameObject;
        protected BaseState(GameObject gameObject)
        {
            GameObject = gameObject;
        }
     
        public virtual void Enter(){}
        public abstract Type Tick();
        public abstract Type FixedTick();
        public virtual void Exit(){}
    }
}