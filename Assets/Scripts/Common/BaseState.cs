using System;
using UnityEngine;

namespace Common
{
    public abstract class BaseState
    {
        protected GameObject GameObject;
        protected StateMachine StateMachine;
        protected BaseState(GameObject gameObject, StateMachine stateMachine)
        {
            GameObject = gameObject;
            StateMachine = stateMachine;
        }

        public virtual void Enter(){}
        public abstract void Tick();
        public abstract void FixedTick();
        public virtual void Exit(){}
    }
}