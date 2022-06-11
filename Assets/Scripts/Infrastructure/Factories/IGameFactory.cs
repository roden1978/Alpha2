using System.Collections.Generic;
using Infrastructure.Services;
using PlayerScripts;
using Services.PersistentProgress;
using Services.Pools;
using StaticData;
using UI;
using UnityEngine;

namespace Infrastructure.Factories
{
    public interface IGameFactory : IService
    {
        ControlsPanel CreateControlsPanel();
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        void CleanUp();
        Crowbar CreateCrowbar();
        Player CreatePlayer();
        Crosshair CreateCrosshair();
        Mediator CreateMediator();
        void AddProgressReader(ISavedProgressReader progressReader);
        void AddProgressWriter(ISavedProgress progressWriter);
        GameObject CreateEnemy(EnemyTypeId enemyTypeId, Transform parent);
        void CreateEnemySpawner(string spawnerId, EnemyTypeId enemyTypeId, Vector3 position,
            Transform parentObjectTransform);
        GameObject CreatePickableObject(PickableObjectTypeId objectTypeId, Transform parent);
        void CreatePickableObjectSpawner(string spawnerId, PickableObjectTypeId pickableObjectTypeId, Vector3 position,
            Transform parentObjectTransform);
        GameObject CreateSavePoint(SaveProgressPointTypeId pointTypeId, float width, float height, Transform parent, bool isUsed);
        void CreateSaveProgressPointSpawner(string spawnerId, SaveProgressPointTypeId pointTypeId, float width,
            float height, Vector3 position, Transform parentObjectTransform);
        PoolService CreatePool();
        public Player Player { get;}
        void CreateHud();
        public Hud Hud { get; }
        ControlsPanel ControlsPanel { get; }
        void CreateGameMenu();
    }
}