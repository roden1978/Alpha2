using Data;
using GameObjectsScripts;
using Infrastructure.Factories;
using PlayerScripts;
using Services.PersistentProgress;
using StaticData;
using UnityEngine;

namespace Infrastructure.PickableObjectSpawners
{
    public class PickableObjectSpawner : MonoBehaviour, ISavedProgress
    {
        private bool _pickedUp;
        private string _id;
        private IGameFactory _gameFactory;
        private PickableObject _pickableObject;
        private PickableObjectTypeId _pickableObjectTypeId;
        public void Construct(GameFactory gameFactory, string spawnerId, PickableObjectTypeId pickableObjectTypeId)
        {
            _id = spawnerId;
            _pickableObjectTypeId = pickableObjectTypeId;
            _gameFactory = gameFactory;
        }

        public void LoadProgress(PlayerProgress playerProgress)
        {
            if (playerProgress.PickableObjectData.ClearedSpawners.Contains(_id))
                _pickedUp = true;
            else
                Spawn();
        }

        private void Spawn()
        {
            GameObject pickableObject = _gameFactory.CreatePickableObject(_pickableObjectTypeId, transform);
            _pickableObject = pickableObject.GetComponent<PickableObject>();
            _pickableObject.PickUp += OnPickUp;
        }

        private void OnPickUp()
        {
            if (_pickableObject != null)
                _pickableObject.PickUp -= OnPickUp;
            _pickedUp = true;
        }

        public void UpdateProgress(PlayerProgress playerProgress)
        {
            if (_pickedUp)
                playerProgress.PickableObjectData.ClearedSpawners.Add(_id);
        }
    }
}