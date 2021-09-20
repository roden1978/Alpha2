using PlayerScripts;
using UnityEngine;

namespace GameScripts
{
    public class Crowbar : MonoBehaviour
    {
        private Player _player;

        private void Start()
        {
            _player = FindObjectOfType<Player>();
        }
    }
}