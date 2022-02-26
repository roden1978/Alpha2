using System;
using Common;
using Infrastructure.AssetManagement;
using Infrastructure.Factories;
using Infrastructure.Services;
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
        private readonly ServiceLocator _serviceLocator;

        public InputInitializeState(GamesStateMachine stateMachine, StatesPayload statesPayload, ServiceLocator serviceLocator)
        {
            _stateMachine = stateMachine;
            _statesPayload = statesPayload;
            _serviceLocator = serviceLocator;
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
            callback?.Invoke(_mobile);

            _serviceLocator.RegisterSingle<IInputService>(InputService());
            _serviceLocator.RegisterSingle<IAssetProvider>(new AssetProvider());
            _serviceLocator.RegisterSingle<IGameFactory>(
                new GameFactory(
                    _serviceLocator.Single<IAssetProvider>())
                );
        }
        private IInputService InputService()
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