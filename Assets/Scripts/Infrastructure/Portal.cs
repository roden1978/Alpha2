using Data;
using Infrastructure.Factories;
using Infrastructure.Services;
using PlayerScripts;
using Services.PersistentProgress;
using Services.SaveLoad;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Portal : MonoBehaviour, ICoroutineRunner, ISavedProgress
    {
        [SerializeField]private BoxCollider2D _boxCollider;
        private ISceneLoader _sceneLoader;
        private ISaveLoadService _saveLoadService;
        private int _currentSceneIndex;
        private IGameFactory _gameFactory;
        private PlayerProgress _playerProgress;

        private void Awake()
        {
            _sceneLoader = new SceneLoader(this);
            _saveLoadService = ServiceLocator.Container.Single<ISaveLoadService>();
            _gameFactory = ServiceLocator.Container.Single<IGameFactory>();
            _playerProgress = ServiceLocator.Container.Single<IPersistentProgressService>().PlayerProgress;
        }

        private void Start()
        {
            _gameFactory.AddProgressWriter(this);
            LoadProgress(_playerProgress);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.TryGetComponent(out Player player))
                Transit(player);
        }

        private void Transit(Component player)
        {
            int newSceneIndex = _currentSceneIndex + 1;
            _sceneLoader.UnLoad(_currentSceneIndex);
            _sceneLoader.Load(newSceneIndex);
            PositionPlayer(player);
            _currentSceneIndex = newSceneIndex;
            
            _saveLoadService.SaveProgress();
        }

        private void PositionPlayer(Component player)
        {
            PlayerSpawnPoint spawnPoint = FindObjectOfType<PlayerSpawnPoint>();
            player.transform.position = spawnPoint.transform.position;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color32(170, 150, 0, 130);
            Gizmos.DrawCube(transform.position, _boxCollider.size);
        }

        public void LoadProgress(PlayerProgress playerProgress)
        {
            _currentSceneIndex = playerProgress.WorldData.PositionOnLevel.SceneIndex;
        }

        public void UpdateProgress(PlayerProgress playerProgress)
        {
            playerProgress.WorldData.PositionOnLevel.SceneIndex = _currentSceneIndex;
            playerProgress.WorldData.PositionOnLevel.SceneName = SceneName();
        }

        private string SceneName()
        {
            return SceneManager.GetSceneByBuildIndex(_currentSceneIndex).name;
        }
    }
}
