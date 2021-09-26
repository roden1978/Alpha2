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
            var state = GetCurrentState()?.Tick();
            AnalyzeState(state);
        }

        private void FixedUpdate()
        {
            var state = GetCurrentState()?.FixedTick();
            AnalyzeState(state);
        }

        private void AnalyzeState(Type state)
        {
            if (state == typeof(EmptyState)) return;
            if (state == GetCurrentState()?.GetType())
            {
                GetCurrentState().Exit();
                PopState();
                PushState(_stack?.FirstOrDefault()?.GetType());
            }
            else
            {
                GetCurrentState().Exit();
                PushState(state);
            }
        }

        private void PushState(Type state)
        {
            _stack.Push(_availableStates[state]);
            GetCurrentState().Enter();
        }

        private void PopState() => _stack.Pop();

        private BaseState GetCurrentState() => _stack?.FirstOrDefault();
    }
}