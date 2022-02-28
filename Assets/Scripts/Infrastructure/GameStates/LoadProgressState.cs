using System;
using Common;
using Data;
using Infrastructure.Services;
using Services.PersistentProgress;
using Services.SaveLoad;

namespace Infrastructure.GameStates
{
    public class LoadProgressState : IState
    {
        private readonly GamesStateMachine _gamesStateMachine;
        private StatesPayload _statesPayload;
        private readonly ServiceLocator _serviceLocator;

        public LoadProgressState(GamesStateMachine gamesStateMachine, StatesPayload statesPayload, ServiceLocator serviceLocator)
        {
            _gamesStateMachine = gamesStateMachine;
            _statesPayload = statesPayload;
            _serviceLocator = serviceLocator;
        }

        public void Enter() => LoadProgress(NextState);

        private void NextState()
        {
            _gamesStateMachine.Enter<InitializeInputState, StatesPayload>(_statesPayload);
        }

        private void LoadProgress(Action callback = null)
        {
            ISaveLoadService saveLoadService = _serviceLocator.Single<ISaveLoadService>();
            IPersistentProgressService persistentProgressService = _serviceLocator.Single<IPersistentProgressService>();
            
            persistentProgressService.PlayerProgress = saveLoadService.LoadProgress() ?? CreatePlayerProgress();
            
            callback?.Invoke();
        }

        private PlayerProgress CreatePlayerProgress()
        {
            _statesPayload.CurrentSceneName = "Level1";
            _statesPayload.CurrentSceneIndex = 1; 
            return new PlayerProgress(sceneName: _statesPayload.CurrentSceneName, sceneIndex: _statesPayload.CurrentSceneIndex);
        }

        public Type Update()
        {
            return null;
        }

        public void Exit()
        {
            //throw new NotImplementedException();
        }
    }
}