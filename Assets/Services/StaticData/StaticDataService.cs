using System.Collections.Generic;
using System.Linq;
using StaticData;
using UnityEngine;

namespace Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<EnemyTypeId,EnemyStaticData> _enemies;

        public void LoadEnemies()
        {
            _enemies = Resources.LoadAll<EnemyStaticData>("EnemyStaticData")
                .ToDictionary(x => x.EnemyTypeId, x => x);
        }

        public EnemyStaticData GetStaticData(EnemyTypeId typeId) =>
            _enemies.TryGetValue(typeId, out EnemyStaticData enemyStaticData)
                ? enemyStaticData
                : null;
    }
}