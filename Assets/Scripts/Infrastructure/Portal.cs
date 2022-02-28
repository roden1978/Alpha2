using PlayerScripts;
using UnityEngine;

namespace Infrastructure
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Portal : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField]private BoxCollider2D _boxCollider;
        private ISceneLoader _sceneLoader;

        private void Awake()
        {
            _sceneLoader = new SceneLoader(this);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.TryGetComponent(out Player player))
                Transit(player);
        }

        private void Transit(Component player)
        {
            player.gameObject.SetActive(false);
            int newSceneIndex = Game.GamePlayerData.CurrentScene + 1;
            _sceneLoader.UnLoad(Game.GamePlayerData.CurrentScene);
            _sceneLoader.Load(newSceneIndex);
            PositionPlayer(player);
            Game.GamePlayerData.CurrentScene = newSceneIndex;
        }

        private void PositionPlayer(Component player)
        {
            PlayerSpawnPoint spawnPoint = FindObjectOfType<PlayerSpawnPoint>();
            player.transform.position = spawnPoint.transform.position;
            player.gameObject.SetActive(true);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color32(170, 150, 0, 130);
            Gizmos.DrawCube(transform.position, _boxCollider.size);
        }
    }
}
