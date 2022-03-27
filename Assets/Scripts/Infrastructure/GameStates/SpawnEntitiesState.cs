using System;
using System.Collections.Generic;
using Common;
using Infrastructure.Factories;
using Infrastructure.Services;
using Services.PersistentProgress;
using Services.StaticData;
using StaticData;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Infrastructure.GameStates
{
    public class SpawnEntitiesState : IState
    {
        private readonly GamesStateMachine _stateMachine;
        private readonly ServiceLocator _serviceLocator;
        private IGameFactory _gameFactory;

        public SpawnEntitiesState(GamesStateMachine stateMachine, ServiceLocator serviceLocator)
        {
            _stateMachine = stateMachine;
            _serviceLocator = serviceLocator;
        }

        public void Enter()
        {
            SpawnGameEntities(OnLoaded);
        }

        private void SpawnGameEntities(Action callback)
        {
            ActivateCurrentScene();
            InitSpawners();
            callback?.Invoke();
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
            _stateMachine.Enter<UpdateProgressState>();
        }

        private void InitSpawners()
        {
            _gameFactory = _serviceLocator.Single<IGameFactory>();
            string levelKey = SceneManager.GetActiveScene().name;
            LevelStaticData levelStaticData = _serviceLocator.Single<IStaticDataService>().GetLevelStaticData(levelKey);
            SpawnEnemies(levelStaticData.EnemySpawners);
            SpawnPickableObjects(levelStaticData.PickableObjectSpawners);
            SpawnSaveProgressPoints(levelStaticData.SaveProgressPointSpawners);
            //SpawnPortal();
        }

        private void SpawnPickableObjects(IEnumerable<PickableObjectSpawnData> pickableObjectSpawners)
        {
            foreach (PickableObjectSpawnData spawnerData in pickableObjectSpawners)
            {
                _gameFactory.CreatePickableObjectSpawner(spawnerData.Id, spawnerData.PickableObjectTypeId,
                    spawnerData.Position);
            }
        }

        private void SpawnEnemies(IEnumerable<EnemySpawnerData> enemySpawners)
        {
            foreach (EnemySpawnerData spawnerData in enemySpawners)
            {
                _gameFactory.CreateEnemySpawner(spawnerData.Id, spawnerData.EnemyTypeId, spawnerData.Position);
            }
        }

        private void SpawnSaveProgressPoints(IEnumerable<SaveProgressPointSpawnData> saveProgressPointSpawners)
        {
            foreach (SaveProgressPointSpawnData spawnerData in saveProgressPointSpawners)
            {
                _gameFactory.CreateSaveProgressPointSpawner(spawnerData.Id, spawnerData.SaveProgressPointTypeId,
                    spawnerData.Width, spawnerData.Height, spawnerData.Position);
            }
        }

        private void SpawnPortal()
        {
            Portal portal = Object.FindObjectOfType<Portal>();
            /*if (portal != null)
                _gameFactory.AddProgressWriter(portal);*/
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