using Data;
using Infrastructure.Factories;
using StaticData;
using UnityEngine;

namespace EnemyScripts
{
    public class LootSpawner : MonoBehaviour
    {
        [SerializeField] private EnemyDeath _enemyDeath;
        [SerializeField] private PickableObjectStaticData _spawnedLootObjectStaticData;
        [SerializeField] [Range(0, 100)] private int _chance = 100;
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
            if(Extensions.Random100() >= _chance)
            {
                GameObject loot =
                    _gameFactory.CreatePickableObject(_spawnedLootObjectStaticData.PickableObjectTypeId, transform);
                loot.transform.parent = null;
            }
        }

        private void OnDestroy()
        {
            _enemyDeath.Death -= OnEnemyDeath;
        }
    }
}