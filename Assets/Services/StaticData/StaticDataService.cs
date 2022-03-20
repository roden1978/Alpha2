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
        private const string LevelStaticDataPath = "LevelStaticData";
        private const string SaveProgressPointStaticDataPath = "SaveProgressPointData";
        private Dictionary<EnemyTypeId, EnemyStaticData> _enemies;
        private Dictionary<PickableObjectTypeId, PickableObjectStaticData> _pickableObjects;
        private Dictionary<string, LevelStaticData> _levelStaticData;
        private Dictionary<SaveProgressPointTypeId, SaveProgressPointStaticData> _saveProgressPoints;


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

        public void LoadLevelStaticData()
        {
            _levelStaticData = Resources.LoadAll<LevelStaticData>(LevelStaticDataPath)
                .ToDictionary(x => x.LevelKey, x => x);
        }

        public void LoadSaveProgressPointStaticData()
        {
            _saveProgressPoints = Resources.LoadAll<SaveProgressPointStaticData>(SaveProgressPointStaticDataPath)
                .ToDictionary(x => x.SaveProgressPointTypeId, x => x);
        }

        public EnemyStaticData GetStaticData(EnemyTypeId typeId) =>
            _enemies.TryGetValue(typeId, out EnemyStaticData enemyStaticData)
                ? enemyStaticData
                : null;

        public PickableObjectStaticData GetPickableObjectStaticData(PickableObjectTypeId typeId) =>
            _pickableObjects.TryGetValue(typeId, out PickableObjectStaticData pickableObjectStaticData)
                ? pickableObjectStaticData
                : null;

        public LevelStaticData GetLevelStaticData(string levelKey)
        {
            return _levelStaticData.TryGetValue(levelKey, out LevelStaticData levelStaticData)
                ? levelStaticData
                : null;
        }

        public SaveProgressPointStaticData GetSaveProgressPointStaticData(SaveProgressPointTypeId pointTypeId)
        {
            return _saveProgressPoints.TryGetValue(pointTypeId,
                out SaveProgressPointStaticData saveProgressPointStaticData)
                ? saveProgressPointStaticData
                : null;
        }
    }
}