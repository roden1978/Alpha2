using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common
{
    public class StateMachine : MonoBehaviour
    {
        private Dictionary<Type, IState> _availableStates;
        private Stack<IState> _stack;
        
        public void Initialize(Dictionary<Type, IState> states)
        {
            _availableStates = states;
            _stack = new Stack<IState>();
            PushState(_availableStates.Values.First().GetType());
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
            if (state == typeof(EmptyState)
                || state == GetCurrentState()?.GetType()) return;
            
            CompletionAndDeleteCurrentState();
            PushState(state);
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

        private void PopState() => _stack?.Pop();

        private IState GetCurrentState() => _stack?.FirstOrDefault();
    }
    
}