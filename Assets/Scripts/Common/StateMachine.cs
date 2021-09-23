using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common
{
    public class StateMachine : MonoBehaviour
    {
        private Dictionary<Type, BaseState> _availableStates;
        private Stack<BaseState> _stack;

        public void Initialize(Dictionary<Type, BaseState> states)
        {
            _availableStates = states;
            _stack = new Stack<BaseState>();
            _stack.Push(_availableStates.Values.First());
        }

        private void Update()
        { 
            GetCurrentState()?.Tick();
            //Debug.Log(GetCurrentState());
        }

        private void FixedUpdate()
        {
            GetCurrentState()?.FixedTick();
        }

        public void PushState(Type nextState)
        {
            if (nextState == null || nextState == GetCurrentState()?.GetType()) return;
            GetCurrentState().Exit();
            _stack.Push(_availableStates[nextState]);
            GetCurrentState().Enter();
        }

        public void PopState() => _stack.Pop();

        private BaseState GetCurrentState() => _stack?.FirstOrDefault();
    }
}