using Data;
using GameObjectsScripts;
using Infrastructure.Factories;
using Infrastructure.Services;
using PlayerScripts;
using Services.PersistentProgress;
using StaticData;
using UnityEngine;

namespace Infrastructure
{
    public class PickableObjectSpawner : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private bool _pickedUp;
        [SerializeField] private PickableObjectStaticData _pickableObjectStaticData;
        private string _id;
        private PlayerProgress _playerProgress;
        private IGameFactory _gameFactory;
        private PickableObject _pickableObject;
        public PickableObjectStaticData PickableObjectStaticData => _pickableObjectStaticData;
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
            if (playerProgress.PickableObjectData.ClearedSpawners.Contains(_id))
                _pickedUp = true;
            else
                Spawn();
        }

        private void Spawn()
        {
            GameObject pickableObject = _gameFactory.CreatePickableObject(_pickableObjectStaticData.PickableObjectTypeId, transform);
            _pickableObject = pickableObject.GetComponent<PickableObject>();
            _pickableObject.PickUp += OnPickUp;
        }

        private void OnPickUp()
        {
            Debug.Log("PickedUp");
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