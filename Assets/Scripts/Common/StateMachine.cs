using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public sealed class StateMachine
    {
        private Dictionary<Type, IState> _availableStates;
        private readonly Stack<IState> _stack;

        public StateMachine()
        {
            _stack = new Stack<IState>();
        }
        public void Initialize(Dictionary<Type, IState> states)
        {
            _availableStates = states;
            PushState(_availableStates.Values.First().GetType());
        }

        public void Update()
        { 
            Type state = GetCurrentState()?.Update();
            AnalyzeState(state);
        }

        private void AnalyzeState(Type state)
        {
            if (state == typeof(EmptyState)
                || state == GetCurrentState()?.GetType()) return;
            
            CompletionAndRemoveCurrentState();
            PushState(state);
        }

        private void CompletionAndRemoveCurrentState()
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

        private IState GetCurrentState() => _stack?.Peek();
    }
    
}