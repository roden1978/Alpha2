using Data;
using PlayerScripts;
using Services.PersistentProgress;
using UnityEngine;

namespace GameObjectsScripts
{
    public class Tar : MonoBehaviour, ISavedProgressReader
    {
        private PlayerState _playerState;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player player))
            {
                player.TakeDamage(_playerState.MaxHealth);
            }
        }

        public void LoadProgress(PlayerProgress playerProgress)
        {
            _playerState = playerProgress.PlayerState;
        }
    }
}
