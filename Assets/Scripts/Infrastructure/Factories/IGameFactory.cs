using System.Collections.Generic;
using GameObjectsScripts;
using Infrastructure.Services;
using PlayerScripts;
using Services.PersistentProgress;
using StaticData;
using UI;
using UnityEngine;

namespace Infrastructure.Factories
{
    public interface IGameFactory : IService
    {
        public ControlsPanel CreateControlsPanel();
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
        GameObject CreatePickableObject(PickableObjectTypeId objectTypeId, Transform parent);
        void CreateSpawner(string spawnerId, EnemyTypeId enemyTypeId, Vector3 position);
        void CreatePickableObjectSpawner(string spawnerId, PickableObjectTypeId pickableObjectTypeId, Vector3 position);
    }
}