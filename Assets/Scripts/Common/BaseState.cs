using System;
using UnityEngine;

namespace Common
{
    public abstract class BaseState
    {
        private readonly GameObject _gameObject;
        protected BaseState(GameObject player)
        {
            _gameObject = player;
        }
     
        public virtual void Enter(){}
        public abstract Type Tick();
        public abstract Type FixedTick();
        public virtual void Exit(){}
    }
}