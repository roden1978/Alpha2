using PlayerScripts;
using UnityEngine;

namespace GameScripts
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private Player _player;
        private void Start()
        {
            Debug.Log("Create Game");
            CreatePlayer();
        }

        private void CreatePlayer()
        {
            Instantiate(_player, Vector3.zero, Quaternion.identity);
        }
    }
}
