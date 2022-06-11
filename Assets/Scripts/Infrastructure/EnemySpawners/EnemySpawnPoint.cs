using Data;
using EnemyScripts;
using Infrastructure.Factories;
using Infrastructure.PickableObjectSpawners;
using Services.PersistentProgress;
using StaticData;
using UnityEngine;

namespace Infrastructure.EnemySpawners
{
    public class EnemySpawnPoint : MonoBehaviour, ISavedProgress, IActivator
    {
        private string _spawnerId;
        private EnemyTypeId _enemyTypeId;
        
        private bool _slain;
        private IGameFactory _gameFactory;
        private EnemyDeath _enemyDeath;
       
        public void Construct(IGameFactory gameFactory, string id, EnemyTypeId enemyTypeId)
        {
            _gameFactory = gameFactory;
            _spawnerId = id;
            _enemyTypeId = enemyTypeId;
        }

        public void LoadProgress(PlayerProgress playerProgress)
        {
            if (playerProgress.KillData.ClearedSpawners.Contains(_spawnerId))
                _slain = true;
            else
                Spawn();
        }

        private void Spawn()
        {
            GameObject enemy = _gameFactory.CreateEnemy(_enemyTypeId, transform);
            _enemyDeath = enemy.GetComponent<EnemyDeath>();
            _enemyDeath.Death += OnEnemyDeath;
            Disable();
        }

        private void OnEnemyDeath()
        {
            if (_enemyDeath != null)
                _enemyDeath.Death -= OnEnemyDeath;
            _slain = true;
        }

        public void UpdateProgress(PlayerProgress playerProgress)
        {
            if (_slain)
                playerProgress.KillData.ClearedSpawners.Add(_spawnerId);
        }

        public void Enable()
        {
            if(!_slain)
                _enemyDeath.gameObject.SetActive(true);
        }

        public void Disable()
        {
            if(!_slain)
                _enemyDeath.gameObject.SetActive(false);
        }
    }
}