using System.Collections.Generic;
using GameObjectsScripts;
using Infrastructure.Services;
using PlayerScripts;
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
        PickableObject CreateLoot();
    }
}