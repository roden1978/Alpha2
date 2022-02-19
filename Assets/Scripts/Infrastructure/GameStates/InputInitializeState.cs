using System;
using Common;
using Input;
using Services.Input;
using UnityEngine;

namespace Infrastructure.GameStates
{
    public class InputInitializeState : IState
    {
        private readonly GamesStateMachine _stateMachine;
        private readonly StatesPayload _statesPayload;
        private bool _mobile;
        public InputInitializeState(GamesStateMachine stateMachine, StatesPayload statesPayload)
        {
            _stateMachine = stateMachine;
            _statesPayload = statesPayload;
        }
        public void Enter()
        {
            RegisterServices(NextState);
        }

        public Type Update()
        {
            return null;
        }

        public void Exit()
        {
            
        }

        private void RegisterServices(Action<bool> callback = null)
        {
            Game.InputService = RegisterInputService();
            callback?.Invoke(_mobile);
        }
        private IInputService RegisterInputService()
        {
            if (Application.isEditor)
                return new KeyboardInputService();

            _mobile = true;
            return new UiInputService();
        }

        private void NextState(bool mobile)
        {
            if (_mobile)
                _stateMachine.Enter<LoadControlsPanelState, StatesPayload>(_statesPayload);
            else
                _stateMachine.Enter<LoadLevelState, StatesPayload>(_statesPayload);
        }
    }
}