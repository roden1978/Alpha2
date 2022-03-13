using System.Collections.Generic;
using System.Linq;
using StaticData;
using UnityEngine;

namespace Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string EnemyStaticDataPath = "EnemyStaticData";
        private const string PickableObjectStaticDataPath = "PickableObjectStaticData";
        private Dictionary<EnemyTypeId, EnemyStaticData> _enemies;
        private Dictionary<PickableObjectTypeId, PickableObjectStaticData> _pickableObjects;

        public void LoadEnemies()
        {
            _enemies = Resources.LoadAll<EnemyStaticData>(EnemyStaticDataPath)
                .ToDictionary(x => x.EnemyTypeId, x => x);
        }

        public void LoadPickableObjects()
        {
            _pickableObjects = Resources.LoadAll<PickableObjectStaticData>(PickableObjectStaticDataPath)
                .ToDictionary(x => x.PickableObjectTypeId, x => x);
        }

        public EnemyStaticData GetStaticData(EnemyTypeId typeId) =>
            _enemies.TryGetValue(typeId, out EnemyStaticData enemyStaticData)
                ? enemyStaticData
                : null;

        public PickableObjectStaticData GetPickableObjectStaticData(PickableObjectTypeId typeId) =>
            _pickableObjects.TryGetValue(typeId, out PickableObjectStaticData pickableObjectStaticData)
                ? pickableObjectStaticData
                : null;
    }
}