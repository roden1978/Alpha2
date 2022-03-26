using Data;
using Infrastructure.Factories;
using Services.PersistentProgress;
using StaticData;
using UnityEngine;

namespace Infrastructure.SavePointSpawners
{
    public class SaveProgressPointSpawner : MonoBehaviour, ISavedProgress
    {
        private bool _isUsed;
        private string _spawnerId;
        private IGameFactory _gameFactory;
        private SaveProgressPoint _saveProgressPoint;
        private float _width;
        private float _height;
        private SaveProgressPointTypeId _pointTypeId;

        public void Construct(GameFactory gameFactory, string spawnerId, float width, float height,
            SaveProgressPointTypeId pointTypeId)
        {
            _spawnerId = spawnerId;
            _width = width;
            _height = height;
            _gameFactory = gameFactory;
            _pointTypeId = pointTypeId;
        }

        public void LoadProgress(PlayerProgress playerProgress)
        {
            if (playerProgress.SaveProgressPointData.UsedSavePoints.Contains(_spawnerId))
            {
                _isUsed = true;
            }

            Spawn();
        }

        private void Spawn()
        {
            GameObject savePoint = _gameFactory.CreateSavePoint(_pointTypeId, _width, _height, transform, _isUsed);
            _saveProgressPoint = savePoint.GetComponent<SaveProgressPoint>();
            _saveProgressPoint.Used += OnUsed;
        }

        private void OnUsed()
        {
            if (_saveProgressPoint != null)
                _saveProgressPoint.Used -= OnUsed;
            _isUsed = true;
            Debug.Log("Progress saved");
        }

        public void UpdateProgress(PlayerProgress playerProgress)
        {
            if (_isUsed)
                playerProgress.SaveProgressPointData.UsedSavePoints.Add(_spawnerId);
        }
    }
}