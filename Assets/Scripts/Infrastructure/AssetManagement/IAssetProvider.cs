﻿using GameObjectsScripts;
using Infrastructure.EnemySpawners;
using Infrastructure.PickableObjectSpawners;
using Infrastructure.SavePointSpawners;
using Infrastructure.Services;
using PlayerScripts;
using Services.Pools;
using UI;
using UnityEngine;

namespace Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        ControlsPanel InstantiateControlsPanel();
        Crowbar InstantiateCrowbar();
        Player InstantiatePlayer();
        Crosshair InstantiateCrosshair();
        Mediator InstantiateMediator();
        PickableObject InstantiateLoot();
        EnemySpawnPoint InstantiateEnemySpawner(Vector3 position, Transform parentObjectTransform);
        PickableObjectSpawner InstantiatePickableObjectSpawner(Vector3 position, Transform parentObjectTransform);
        SaveProgressPointSpawner InstantiateSaveProgressSpawner(Vector3 position, Transform parentObjectTransform);
        PoolService InstantiatePool();
        Hud InstantiateHud();
        GameMenu InstantiateGameMenu();
    }
}