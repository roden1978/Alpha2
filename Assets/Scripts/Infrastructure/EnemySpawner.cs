using Data;
using EnemyScripts;
using GameObjectsScripts;
using Infrastructure.Factories;
using Infrastructure.Services;
using PlayerScripts;
using Services.PersistentProgress;
using StaticData;
using UnityEngine;

namespace Infrastructure
{
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private bool _slain;
        [SerializeField] private EnemyStaticData _enemyStaticData;
        private string _id;
        private PlayerProgress _playerProgress;
        private IGameFactory _gameFactory;
        private EnemyDeath _enemyDeath;
        public EnemyStaticData EnemyStaticData => _enemyStaticData;
        private void Awake()
        {
            _id = GetComponent<UniqueId>().Id;
            _playerProgress = ServiceLocator.Container.Single<IPersistentProgressService>().PlayerProgress;
            _gameFactory = ServiceLocator.Container.Single<IGameFactory>();
        }

        private void Start()
        {
            LoadProgress(_playerProgress);
        }

        public void LoadProgress(PlayerProgress playerProgress)
        {
            if (playerProgress.KillData.ClearedSpawners.Contains(_id))
                _slain = true;
            else
                Spawn();
        }

        private void Spawn()
        {
            GameObject enemy = _gameFactory.CreateEnemy(_enemyStaticData.EnemyTypeId, transform);
            _enemyDeath = enemy.GetComponent<EnemyDeath>();
            _enemyDeath.Death += OnEnemyDeath;
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
                playerProgress.KillData.ClearedSpawners.Add(_id);
        }
    }
}