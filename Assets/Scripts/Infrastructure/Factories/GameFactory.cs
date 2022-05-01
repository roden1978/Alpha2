using System.Collections.Generic;
using EnemyScripts;
using GameObjectsScripts;
using Infrastructure.AssetManagement;
using Infrastructure.EnemySpawners;
using Infrastructure.PickableObjectSpawners;
using Infrastructure.SavePointSpawners;
using PlayerScripts;
using Services.PersistentProgress;
using Services.Pools;
using Services.StaticData;
using StaticData;
using UI;
using UnityEngine;

namespace Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        public Player Player { get; private set; }
        public Hud Hud { get; private set; }
        public ControlsPanel ControlsPanel { get; private set; }
        public GameMenu GameMenu { get; private set; }

        public List<ISavedProgressReader> ProgressReaders { get; }

        public List<ISavedProgress> ProgressWriters { get; }

        private readonly IAssetProvider _assetProvider;

        private readonly IStaticDataService _staticDataService;

        public GameFactory(IAssetProvider assetProvider, IStaticDataService staticDataService)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            ProgressReaders = new List<ISavedProgressReader>();
            ProgressWriters = new List<ISavedProgress>();
        }

        public void CreateHud()
        {
            Hud = _assetProvider.InstantiateHud();
            RegisterInSaveLoadRepositories(Hud.gameObject);
        }

        public ControlsPanel CreateControlsPanel()
        {
            ControlsPanel = _assetProvider.InstantiateControlsPanel();
            return ControlsPanel;
        }

        public void CreateGameMenu()
        {
           GameMenu = _assetProvider.InstantiateGameMenu();
           GameMenu.Construct(ControlsPanel);
        }

        public Crowbar CreateCrowbar()
        {
            Crowbar crowbar = _assetProvider.InstantiateCrowbar();
            FootstepFX footstepFx = Player.GetComponent<FootstepFX>();
            GroundingFx groundingFx = Player.GetComponent<GroundingFx>();
            JumpFx jumpFx = Player.GetComponent<JumpFx>();
            crowbar.Construct(Player, _staticDataService, footstepFx, groundingFx, jumpFx);
            RegisterInSaveLoadRepositories(crowbar.gameObject);
            if(ControlsPanel != null)
                ControlsPanel.Construct(crowbar);
            return crowbar;
        }

        public PoolService CreatePool()
        {
            PoolService pool = _assetProvider.InstantiatePool();
            return pool;
        }

        public Player CreatePlayer()
        {
            Player = _assetProvider.InstantiatePlayer();
            RegisterInSaveLoadRepositories(Player.gameObject);
            return Player;
        }

        public Crosshair CreateCrosshair() => _assetProvider.InstantiateCrosshair();

        public Mediator CreateMediator()
        {
            Mediator mediator = _assetProvider.InstantiateMediator();
            RegisterInSaveLoadRepositories(mediator.gameObject);
            return mediator;
        }

        public void CreateEnemySpawner(string spawnerId, EnemyTypeId enemyTypeId, Vector3 position)
        {
            EnemySpawnPoint spawnPoint = _assetProvider.InstantiateEnemySpawner(position);
            spawnPoint.Construct(this, spawnerId, enemyTypeId);
            RegisterInSaveLoadRepositories(spawnPoint.gameObject);
        }

        public void CreatePickableObjectSpawner(string spawnerId, PickableObjectTypeId pickableObjectTypeId,
            Vector3 position)
        {
            PickableObjectSpawner pickableObjectSpawner = _assetProvider.InstantiatePickableObjectSpawner(position);
            pickableObjectSpawner.Construct(this, spawnerId, pickableObjectTypeId);
            RegisterInSaveLoadRepositories(pickableObjectSpawner.gameObject);
        }

        public void CreateSaveProgressPointSpawner(string spawnerId, SaveProgressPointTypeId pointTypeId, float width,
            float height, Vector3 position)
        {
            SaveProgressPointSpawner saveProgressPointSpawner = _assetProvider.InstantiateSaveProgressSpawner(position);
            saveProgressPointSpawner.Construct(this, spawnerId, width, height, pointTypeId);
            RegisterInSaveLoadRepositories(saveProgressPointSpawner.gameObject);
        }

        private void RegisterInSaveLoadRepositories(GameObject registeredGameObject)
        {
            foreach (ISavedProgressReader progressReader in registeredGameObject
                .GetComponentsInChildren<ISavedProgressReader>())
            {
                if (progressReader is ISavedProgress progressWriter)
                    AddProgressWriter(progressWriter);
                AddProgressReader(progressReader);
            }
        }

        public void AddProgressReader(ISavedProgressReader progressReader)
        {
            ProgressReaders.Add(progressReader);
        }

        public void AddProgressWriter(ISavedProgress progressWriter)
        {
            ProgressWriters.Add(progressWriter);
        }

        public GameObject CreateEnemy(EnemyTypeId enemyTypeId, Transform parent)
        {
            EnemyStaticData enemyStaticData = _staticDataService.GetStaticData(enemyTypeId);
            GameObject enemy = Object.Instantiate(enemyStaticData.Prefab, parent.position, Quaternion.identity, parent);

            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            enemyHealth.Construct(enemyStaticData.Health);

            ActorUI actorUI = enemy.GetComponent<ActorUI>();
            actorUI.Construct(enemyHealth);

            if(enemy.TryGetComponent(out Aggro aggro))
                aggro.Construct(enemyStaticData.Cooldown);

            LootSpawner loopSpawner = enemy.GetComponentInChildren<LootSpawner>();
            loopSpawner.Construct(this);

            return enemy;
        }

        public GameObject CreateSavePoint(SaveProgressPointTypeId saveProgressPointTypeId, float width, float height,
            Transform parent, bool isUsed)
        {
            SaveProgressPointStaticData saveProgressPointStaticData =
                _staticDataService.GetSaveProgressPointStaticData(saveProgressPointTypeId);
            SaveProgressPoint saveProgressPoint = Object.Instantiate(saveProgressPointStaticData.Prefab,
                parent.position,
                Quaternion.identity, parent);
            saveProgressPoint.Construct(width, height, isUsed);
            return saveProgressPoint.gameObject;
        }

        public GameObject CreatePickableObject(PickableObjectTypeId objectTypeId, Transform parent)
        {
            PickableObjectStaticData pickableObjectStaticData =
                _staticDataService.GetPickableObjectStaticData(objectTypeId);
            GameObject gameObject = Object.Instantiate(pickableObjectStaticData.Prefab, parent.position,
                Quaternion.identity, parent);
            PickableObject pickableObject = gameObject.GetComponent<PickableObject>();
            pickableObject.Value = pickableObjectStaticData.Value;
            return gameObject;
        }

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }
    }
}