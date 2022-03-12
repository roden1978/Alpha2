using System;
using Data;
using GameObjectsScripts;
using Infrastructure.Factories;
using Infrastructure.Services;
using PlayerScripts;
using Services.PersistentProgress;
using Services.StaticData;
using UnityEngine;

namespace Infrastructure
{
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private EnemyTypeId _enemyTypeId;
        private string _id;
        public bool Slain;
        private PlayerProgress _playerProgress;

        private void Awake()
        {
            _id = GetComponent<UniqueId>().Id;
            _playerProgress = ServiceLocator.Container.Single<IPersistentProgressService>().PlayerProgress;
        }

        private void Start()
        {
            LoadProgress(_playerProgress);
        }

        public void LoadProgress(PlayerProgress playerProgress)
        {
            Debug.Log("ID: "+_id);
            if (playerProgress.KillData.ClearedSpawners.Contains(_id))
                Slain = true;
            else
                Spawn();
        }

        private void Spawn()
        {
            
        }

        public void UpdateProgress(PlayerProgress playerProgress)
        {
            if(Slain)
                playerProgress.KillData.ClearedSpawners.Add(_id);
        }
    }
}