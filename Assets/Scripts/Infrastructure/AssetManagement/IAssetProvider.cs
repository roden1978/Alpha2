using GameObjectsScripts;
using Infrastructure.EnemySpawners;
using Infrastructure.Services;
using PlayerScripts;
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
        EnemySpawnPoint InstantiateEnemySpawner(Vector3 position);
    }
}