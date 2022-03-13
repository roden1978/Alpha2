using System;
using GameObjectsScripts;
using Infrastructure.Factories;
using UnityEngine;

namespace EnemyScripts
{
    public class LootSpawner : MonoBehaviour
    {
        [SerializeField] private EnemyDeath _enemyDeath;
        private IGameFactory _gameFactory;

        public void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        private void Start()
        {
            _enemyDeath.Death += OnEnemyDeath;
        }

        private void OnEnemyDeath()
        {
            PickableObject loot = _gameFactory.CreateLoot();
            loot.transform.position = transform.position;
        }
    }
}