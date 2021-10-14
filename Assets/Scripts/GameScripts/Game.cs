using Cinemachine;
using PlayerScripts;
using UnityEngine;

namespace GameScripts
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private CinemachineVirtualCamera _vCamera;
        private PlayerSpawnPoint _playerSpawnPoint;

        private void Start()
        {
            _playerSpawnPoint = FindObjectOfType<PlayerSpawnPoint>();
            var spawnPoint = _playerSpawnPoint ? _playerSpawnPoint.transform.position : Vector3.zero;
            var player = CreatePlayer(_player, spawnPoint);
            CreateCrowBar();
            _vCamera.Follow = player.transform;
        }

        private void OnValidate()
        {
            if(_player == null) 
                Debug.LogError("Select Player Prefab to the Game Script!");
            
            if(transform.position != Vector3.zero)
                transform.position = Vector3.zero;
        }

        private Player CreatePlayer(Player playerPrefab, Vector3 spawnPoint)
        {
            if(spawnPoint == Vector3.zero) 
                spawnPoint =  new FloatingPlayerSpawnPoint().Value();
            var player = Instantiate(playerPrefab, spawnPoint, Quaternion.identity);
            player.name = "Player";
            return player;
        }

        private void CreateCrowBar()
        {
            var crowbar = new GameObject("Crowbar", typeof(Crowbar));
            crowbar.transform.SetParent(transform);
        }
    }
}
