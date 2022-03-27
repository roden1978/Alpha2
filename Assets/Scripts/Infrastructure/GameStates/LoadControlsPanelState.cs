using System;
using Common;
using Infrastructure.Factories;
using Infrastructure.Services;
using Services.PersistentProgress;

namespace Infrastructure.GameStates
{
    public class LoadControlsPanelState : IState
    {
        private readonly GamesStateMachine _stateMachine;
        private readonly ServiceLocator _serviceLocator;

        public LoadControlsPanelState(GamesStateMachine stateMachine, ServiceLocator serviceLocator)
        {
            _stateMachine = stateMachine;
            _serviceLocator = serviceLocator;
        }

        public Type Update()
        {
            return null;
        }

        public void Exit()
        {
        }

        public void Enter()
        {
            LoadControlsPanel(OnLoaded);
        }

        private void OnLoaded()
        {
            string levelName = _serviceLocator.Single<IPersistentProgressService>().PlayerProgress.WorldData
                .PositionOnLevel.SceneName;
            _stateMachine.Enter<LoadLevelState, string>(levelName);
        }

        private void LoadControlsPanel(Action onLoaded)
        {
            _serviceLocator.Single<IGameFactory>().CreateControlsPanel();
            onLoaded?.Invoke();
        }
    }
}