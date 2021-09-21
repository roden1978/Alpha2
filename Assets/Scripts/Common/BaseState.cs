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

        protected abstract void Enter();
        public abstract Type Tick();
        public abstract Type FixedTick();
        protected abstract void Exit();
    }
}