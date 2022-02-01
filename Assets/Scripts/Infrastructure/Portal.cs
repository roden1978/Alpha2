using PlayerScripts;
using UnityEngine;

namespace Infrastructure
{
    public class Portal : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.TryGetComponent(out Player player))
                player.TransitionToNextScene();
        }
    }
}
