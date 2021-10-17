using System;
using UnityEngine;

namespace Common
{
    public abstract class BaseState
    {
        private readonly GameObject _gameObject;
        protected readonly int JumpThrow = Animator.StringToHash("JumpThrow");
        protected readonly int Jump = Animator.StringToHash("Jump");
        protected readonly int Walk = Animator.StringToHash("Walk");
        protected readonly int IdleThrow = Animator.StringToHash("IdleThrow");
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