using System;
using Common;
using Infrastructure.Factories;
using Infrastructure.Services;
using PlayerScripts;
using Services.PersistentProgress;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Infrastructure.GameStates
{
    public class UpdateProgressState : IState
    {
        private readonly ServiceLocator _serviceLocator;
        private IGameFactory _gameFactory;

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

        private void InitSpawners()
        {
            var spawners = Object.FindObjectsOfType<EnemySpawner>();
            foreach (EnemySpawner spawner in spawners)
            {
                _gameFactory.AddProgressWriter(spawner);
            }
        }
        private void InitPortal()
        {
            Portal portal = Object.FindObjectOfType<Portal>();
            if(portal != null)
                _gameFactory.AddProgressWriter(portal);
        }

        private void UpdatePlayerProgress()
        {
            ActivateCurrentScene();
            _gameFactory = _serviceLocator.Single<IGameFactory>();
            IPersistentProgressService persistentProgressService = _serviceLocator.Single<IPersistentProgressService>();
            //refactor this registration object from loading scene to update
            InitSpawners();
            InitPortal();

            foreach (ISavedProgressReader readers in _gameFactory.ProgressReaders)
            {
                readers.LoadProgress(persistentProgressService.PlayerProgress);
            }
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