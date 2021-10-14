using PlayerScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameObjectsScripts
{
    public class Tar : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player player))
            {
                Debug.Log("Player is die");
                SceneManager.LoadSceneAsync("Level1", LoadSceneMode.Additive);
            }
        }
    }
}
