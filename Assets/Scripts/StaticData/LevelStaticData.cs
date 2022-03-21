using System.Collections.Generic;
using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "New LevelData", menuName = "StaticData/LevelData")]
    public class LevelStaticData : ScriptableObject
    {
        public string LevelKey;
        public List<EnemySpawnerData> EnemySpawners;
        public List<PickableObjectSpawnData> PickableObjectSpawners;
        public List<SaveProgressPointSpawnData> SaveProgressPointSpawners;
    }
}