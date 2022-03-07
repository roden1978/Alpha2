using Infrastructure;
using PlayerScripts;
using UnityEngine;

namespace GameObjectsScripts
{
    public class Tar : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player player))
            {
                player.TakeDamage(Game.GamePlayerData.MaxHealth);
            }
        }
    }
}
