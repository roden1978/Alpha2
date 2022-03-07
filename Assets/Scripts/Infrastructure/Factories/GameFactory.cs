using System;
using System.Collections.Generic;
using Infrastructure.AssetManagement;
using PlayerScripts;
using UI;
using UnityEngine;

namespace Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        public List<ISavedProgressReader> ProgressReaders { get; }
        public List<ISavedProgress> ProgressWriters { get; }

        public GameFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
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

        public Player CreatePlayer() => _assetProvider.InstantiatePlayer();
        public Crosshair CreateCrosshair() => _assetProvider.InstantiateCrosshair();

        private void RegisterInSaveLoadRepositories(GameObject registeredGameObject)
        {
            foreach (ISavedProgressReader progressReader in registeredGameObject.GetComponentsInChildren<ISavedProgressReader>())
            {
                if(progressReader is ISavedProgress progressWriter)
                    ProgressWriters.Add(progressWriter);
                ProgressReaders.Add(progressReader);
            }
        }

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }
    }
}