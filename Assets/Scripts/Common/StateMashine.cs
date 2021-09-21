using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common
{
    public class StateMashine : MonoBehaviour
    {
        private Dictionary<Type, BaseState> _availableStates;
        public BaseState CurrentState { get; private set; }
        public BaseState PrevState { get; private set; }

        public void Initialize(Dictionary<Type, BaseState> states)
        {
            _availableStates = states;
            CurrentState ??= _availableStates.Values.First();
        }

        private void Update()
        {
            var nextState = CurrentState?.Tick();
            SwitchToNewState(nextState);
        }

        private void FixedUpdate()
        {
            var nextState = CurrentState?.FixedTick();
            SwitchToNewState(nextState);
        }

        private void SwitchToNewState(Type nextState)
        {
            if (nextState == CurrentState?.GetType()) return;
            PrevState = CurrentState;
            CurrentState = _availableStates[nextState];
        }

        //public Type GetPrevState => PrevState.GetType();
    }
}