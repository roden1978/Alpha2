using System;
using Common;
using Infrastructure.Factories;
using Infrastructure.Services;
using PlayerScripts;
using Services.PersistentProgress;
using Services.StaticData;
using StaticData;
using UnityEngine;
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
            /*var enemySpawners = Object.FindObjectsOfType<EnemySpawner>();
            foreach (EnemySpawner spawner in enemySpawners)
            {
                _gameFactory.AddProgressWriter(spawner);
            }*/

            string levelKey = SceneManager.GetActiveScene().name;
            Debug.Log(levelKey);
            LevelStaticData levelStaticData = _serviceLocator.Single<IStaticDataService>().GetLevelStaticData(levelKey);
            _gameFactory = _serviceLocator.Single<IGameFactory>();
            foreach (EnemySpawnerData spawnerData in levelStaticData.EnemySpawners)
            {
                _gameFactory.CreateSpawner(spawnerData.Id, spawnerData.EnemyTypeId, spawnerData.Position);
            }
            
            var pickableObjectSpawners = Object.FindObjectsOfType<PickableObjectSpawner>();
            foreach (PickableObjectSpawner spawner in pickableObjectSpawners)
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