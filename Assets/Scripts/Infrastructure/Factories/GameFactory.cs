using System.Collections.Generic;
using EnemyScripts;
using GameObjectsScripts;
using Infrastructure.AssetManagement;
using PlayerScripts;
using Services.StaticData;
using StaticData;
using UI;
using UnityEngine;

namespace Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticData;
        public List<ISavedProgressReader> ProgressReaders { get; }
        public List<ISavedProgress> ProgressWriters { get; }

        public GameFactory(IAssetProvider assetProvider, IStaticDataService staticData)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
            ProgressReaders = new List<ISavedProgressReader>();
            ProgressWriters = new List<ISavedProgress>();
        }
        public ControlsPanel CreateControlsPanel() => 
            _assetProvider.InstantiateControlsPanel();

        public Crowbar CreateCrowbar()
        {
            Crowbar crowbar = _assetProvider.InstantiateCrowbar();
            RegisterInSaveLoadRepositories(crowbar.gameObject);
            return crowbar;
        }

        public Player CreatePlayer()
        {
            Player player = _assetProvider.InstantiatePlayer();
            RegisterInSaveLoadRepositories(player.gameObject);
            return player;
        }

        public Crosshair CreateCrosshair() => _assetProvider.InstantiateCrosshair();
        public Mediator CreateMediator()
        {
            Mediator mediator = _assetProvider.InstantiateMediator();
            RegisterInSaveLoadRepositories(mediator.gameObject);
            return mediator;
        }

        private void RegisterInSaveLoadRepositories(GameObject registeredGameObject)
        {
            foreach (ISavedProgressReader progressReader in registeredGameObject.GetComponentsInChildren<ISavedProgressReader>())
            {
                if(progressReader is ISavedProgress progressWriter)
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
            EnemyStaticData enemyStaticData = _staticData.GetStaticData(enemyTypeId);
            GameObject enemy = Object.Instantiate(enemyStaticData.Prefab, parent.position, Quaternion.identity, parent);
            
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            enemyHealth.Construct(enemyStaticData.Health);
            
            ActorUI actorUI = enemy.GetComponent<ActorUI>();
            actorUI.Construct(enemyHealth);

            Aggro aggro = enemy.GetComponent<Aggro>();
            aggro.Construct(enemyStaticData.Cooldown);

            LootSpawner loopSpawner = enemy.GetComponentInChildren<LootSpawner>();
            loopSpawner.Construct(this);
            
            return enemy;
        }

        public PickableObject CreateLoot()
        {
            PickableObject loot = _assetProvider.InstantiateLoot();
            RegisterInSaveLoadRepositories(loot.gameObject);
            return loot;
        }

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }
    }
}