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
                CompletionAndDeleteCurrentState();
                var prevState = PopState();
                PushState(prevState);
            }
            else
            {
                CompletionAndDeleteCurrentState();
                PushState(state);
            }
        }

        private void CompletionAndDeleteCurrentState()
        {
            GetCurrentState()?.Exit();
            PopState();
        }

        private void PushState(Type state)
        {
            _stack?.Push(_availableStates[state]);
            GetCurrentState()?.Enter();
        }

        private Type PopState()=> _stack?.Count > 0 ? _stack?.Pop().GetType() : typeof(EmptyState);

        private BaseState GetCurrentState() => _stack?.FirstOrDefault();
    }
}