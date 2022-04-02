using System;
using Common;
using Infrastructure.Services;
using Services.Input;
using UnityEngine;

namespace Infrastructure.GameStates
{
    public class InitializeInputState : IState
    {
        private readonly GamesStateMachine _stateMachine;
        private bool _mobile;
        private readonly ServiceLocator _serviceLocator;

        public InitializeInputState(GamesStateMachine stateMachine, ServiceLocator serviceLocator)
        {
            _stateMachine = stateMachine;
            _serviceLocator = serviceLocator;
        }
        public void Enter()
        {
            RegisterInputService(NextState);
        }

        public Type Update()
        {
            return null;
        }

        public void Exit()
        {
            
        }

        private void RegisterInputService(Action callback = null)
        {
            _serviceLocator.RegisterSingle(InputService());
            callback?.Invoke();
        }
        private IInputService InputService()
        {
            //if (Application.isEditor)
             //  return new KeyboardInputService();

            _mobile = true;
            return new UiInputService();
        }

        private void NextState()
        {
            //string levelName = _serviceLocator.Single<IPersistentProgressService>().PlayerProgress.WorldData
             //   .PositionOnLevel.SceneName;
            if (_mobile)
                _stateMachine.Enter<LoadControlsPanelState>();
            //else
            //   _stateMachine.Enter<LoadLevelState, string>("MainMenu");
        }
    }
}