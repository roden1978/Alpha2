using System;
using Common;
using Infrastructure.Services;
using Services.PersistentProgress;
using UnityEngine.SceneManagement;

namespace Infrastructure.GameStates
{
    public class LoadLevelState : IPayloadState<StatesPayload>
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly ServiceLocator _serviceLocator;
        private readonly GamesStateMachine _stateMachine;
        private StatesPayload _statesPayload;

        public LoadLevelState(GamesStateMachine stateMachine, ISceneLoader sceneLoader, ServiceLocator serviceLocator)
        {
            _sceneLoader = sceneLoader;
            _serviceLocator = serviceLocator;
            _stateMachine = stateMachine;
        }
        public void Enter(StatesPayload statesPayload)
        {
            _statesPayload = statesPayload;
            string sceneName = _serviceLocator.Single<IPersistentProgressService>().PlayerProgress.WorldData
                .PositionOnLevel.SceneName;
            LoadScene(sceneName, OnLoaded);
        }

        public Type Update()
        {
            return null;
        }

        public void Exit()
        {
            
        }
        
        private void OnLoaded()
        {
            _stateMachine.Enter<InitializePoolState, StatesPayload>(_statesPayload);
        }
        

        private void LoadScene(string sceneName, Action onLoaded)
        {
            _sceneLoader.Load(sceneName, onLoaded);
        }
        
    }
}