using System;
using Common;
using Infrastructure.Services;
using Input;
using Services.Input;
using UnityEngine;

namespace Infrastructure.GameStates
{
    public class InitializeInputState : IPayloadState<StatesPayload>
    {
        private readonly GamesStateMachine _stateMachine;
        private bool _mobile;
        private readonly ServiceLocator _serviceLocator;

        public InitializeInputState(GamesStateMachine stateMachine, ServiceLocator serviceLocator)
        {
            _stateMachine = stateMachine;
            _serviceLocator = serviceLocator;
        }
        public void Enter(StatesPayload statesPayload)
        {
            RegisterInputService(statesPayload, NextState);
        }

        public Type Update()
        {
            return null;
        }

        public void Exit()
        {
            
        }

        private void RegisterInputService(StatesPayload statesPayload, Action<bool, StatesPayload> callback = null)
        {
            _serviceLocator.RegisterSingle(InputService());
            callback?.Invoke(_mobile, statesPayload);
        }
        private IInputService InputService()
        {
            if (Application.isEditor)
                return new KeyboardInputService();

            _mobile = true;
            return new UiInputService();
        }

        private void NextState(bool mobile, StatesPayload statesPayload)
        {
            if (_mobile)
                _stateMachine.Enter<LoadControlsPanelState, StatesPayload>(statesPayload);
            else
                _stateMachine.Enter<LoadLevelState, StatesPayload>(statesPayload);
        }
    }
}