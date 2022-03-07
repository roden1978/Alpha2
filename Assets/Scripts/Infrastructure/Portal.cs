using Infrastructure.Services;
using PlayerScripts;
using Services.PersistentProgress;
using UnityEngine;

namespace Infrastructure
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Portal : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField]private BoxCollider2D _boxCollider;
        private ISceneLoader _sceneLoader;
        private ServiceLocator _serviceLocator;

        private void Awake()
        {
            _sceneLoader = new SceneLoader(this);
            _serviceLocator = ServiceLocator.Container;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.TryGetComponent(out Player player))
                Transit(player);
        }

        private void Transit(Component player)
        {
            //player.gameObject.SetActive(false);
            int newSceneIndex = _serviceLocator.Single<IPersistentProgressService>().PlayerProgress.WorldData
                .PositionOnLevel.SceneIndex + 1;
            _sceneLoader.UnLoad(_serviceLocator.Single<IPersistentProgressService>().PlayerProgress.WorldData
                .PositionOnLevel.SceneIndex);
            _sceneLoader.Load(newSceneIndex);
            PositionPlayer(player);
            _serviceLocator.Single<IPersistentProgressService>().PlayerProgress.WorldData
                .PositionOnLevel.SceneIndex = newSceneIndex;
        }

        private void PositionPlayer(Component player)
        {
            PlayerSpawnPoint spawnPoint = FindObjectOfType<PlayerSpawnPoint>();
            player.transform.position = spawnPoint.transform.position;
            //player.gameObject.SetActive(true);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color32(170, 150, 0, 130);
            Gizmos.DrawCube(transform.position, _boxCollider.size);
        }
    }
}
