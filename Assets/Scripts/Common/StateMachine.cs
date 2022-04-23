using System;
using System.Collections.Generic;

namespace Common
{
    public class StateMachine
    {
        private readonly Dictionary<Type, List<Transition>> _transitions;
        private readonly List<Transition> _anyTransitions;
        private IState _currentState;
        private List<Transition> _currentTransitions;
        private static List<Transition> EmptyTransitions;

        public StateMachine()
        {
            _transitions = new Dictionary<Type, List<Transition>>();
            _currentTransitions = new List<Transition>();
            _anyTransitions = new List<Transition>();
            EmptyTransitions = new List<Transition>(0);
        }

        public void Update()
        {
            Transition transition = GetTransition();
            if(transition != null)
                SetState(transition.To);

            _currentState?.Update();
        }

        public void SetState(IState state)
        {
            if(state == _currentState) return;

            _currentState?.Exit();

            _currentState = state;
            _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);
        
            _currentTransitions ??= EmptyTransitions;

            _currentState.Enter();
        }

        public void AddTransition(IState from, IState to, ICondition condition)
        {
            bool result = _transitions.TryGetValue(from.GetType(), out var value);
            if(result == false)
            {
                value = new List<Transition>();
            
                Type type = from.GetType();
                _transitions[type] = value;
            }

            value.Add(new Transition(to, condition));
        }
        public void AddAnyTransition(IState to, ICondition condition)
        {
            _anyTransitions.Add(new Transition(to, condition));
        }

        private Transition GetTransition()
        {
            foreach(Transition transition in _anyTransitions)
                if(transition.Condition.Result())
                    return transition;
        
            foreach(Transition transition in _currentTransitions)
                if(transition.Condition.Result())
                    return transition;

            return null;
        }
    }
}
