using System;
using Common;
using Infrastructure.Factories;
using Infrastructure.Services;
using PlayerScripts;
using Services.PersistentProgress;
using UnityEngine.SceneManagement;

namespace Infrastructure.GameStates
{
    public class UpdateProgressState : IState
    {
        private readonly ServiceLocator _serviceLocator;

        public UpdateProgressState(ServiceLocator serviceLocator)
        {
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
            UpdatePlayerProgress();
        }

        private void UpdatePlayerProgress()
        {
            IGameFactory gameFactory = _serviceLocator.Single<IGameFactory>();
            IPersistentProgressService persistentProgressService = _serviceLocator.Single<IPersistentProgressService>();
            foreach (ISavedProgressReader readers in gameFactory.ProgressReaders)
            {
                readers.LoadProgress(persistentProgressService.PlayerProgress);
            }
            ActivateCurrentScene();
        }
        
        private void ActivateCurrentScene()
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(SceneIndex()));
        }

        private int SceneIndex()
        {
            return _serviceLocator.Single<IPersistentProgressService>().PlayerProgress.WorldData
                .PositionOnLevel.SceneIndex;
        }
    }
}