using GameObjectsScripts;
using Infrastructure.EnemySpawners;
using Infrastructure.PickableObjectSpawners;
using Infrastructure.SavePointSpawners;
using PlayerScripts;
using Services.Pools;
using UI;
using UnityEngine;

namespace Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public ControlsPanel InstantiateControlsPanel()
        {
            GameObject prefab = Resources.Load<GameObject>(AssetPaths.ControlsPanelPath);
            return Object.Instantiate(prefab).GetComponent<ControlsPanel>();
        }

        public Crowbar InstantiateCrowbar()
        {
            GameObject prefab = Resources.Load<GameObject>(AssetPaths.CrowbarPath);
            return Object.Instantiate(prefab).GetComponent<Crowbar>();
        }

        public Player InstantiatePlayer()
        {
            GameObject prefab = Resources.Load<GameObject>(AssetPaths.PlayerPath);
            return Object.Instantiate(prefab).GetComponent<Player>();
        }

        public Crosshair InstantiateCrosshair()
        {
            GameObject prefab = Resources.Load<GameObject>(AssetPaths.CrosshairPath);
            return Object.Instantiate(prefab).GetComponent<Crosshair>();
        }

        public Mediator InstantiateMediator()
        {
            GameObject prefab = Resources.Load<GameObject>(AssetPaths.MediatorPath);
            return Object.Instantiate(prefab).GetComponent<Mediator>();
        }

        public PickableObject InstantiateLoot()
        {
            GameObject prefab = Resources.Load<GameObject>(AssetPaths.CrystalPath);
            return Object.Instantiate(prefab).GetComponent<PickableObject>();
        }

        public EnemySpawnPoint InstantiateEnemySpawner(Vector3 position)
        {
            GameObject prefab = Resources.Load<GameObject>(AssetPaths.EnemySpawnPoint);
            return Object.Instantiate(prefab, position, Quaternion.identity).GetComponent<EnemySpawnPoint>();
        }

        public PickableObjectSpawner InstantiatePickableObjectSpawner(Vector3 position)
        {
            GameObject prefab = Resources.Load<GameObject>(AssetPaths.PickableObjectSpawnPoint);
            return Object.Instantiate(prefab, position, Quaternion.identity).GetComponent<PickableObjectSpawner>();
        }

        public SaveProgressPointSpawner InstantiateSaveProgressSpawner(Vector3 position)
        {
            GameObject prefab = Resources.Load<GameObject>(AssetPaths.SaveProgressSpawnPoint);
            return Object.Instantiate(prefab, position, Quaternion.identity).GetComponent<SaveProgressPointSpawner>();
        }

        public PoolService InstantiatePool()
        {
            GameObject prefab = Resources.Load<GameObject>(AssetPaths.PoolPath);
            return Object.Instantiate(prefab).GetComponent<PoolService>();
        }

        public Hud InstantiateHud()
        {
            GameObject prefab = Resources.Load<GameObject>(AssetPaths.HudPath);
            return Object.Instantiate(prefab).GetComponent<Hud>();
        }

        public GameMenu InstantiateGameMenu()
        {
            GameObject prefab = Resources.Load<GameObject>(AssetPaths.GameMenuPath);
            return Object.Instantiate(prefab).GetComponent<GameMenu>();
        }
    }
}