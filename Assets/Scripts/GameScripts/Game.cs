using System.Collections;
using Input;
using PlayerScripts;
using UnityEngine;

namespace GameScripts
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private PlayerSpawnPoint _playerSpawnPoint;
        private void Start()
        {
            var spawnPoint = _playerSpawnPoint ? _playerSpawnPoint.transform.position : Vector3.zero;
            StartCoroutine(CreatePlayer(_player, spawnPoint));
            StartCoroutine(CreateCrowBar());
        }

        private void OnValidate()
        {
            if(_player == null) 
                Debug.LogError("Select Player Prefab to the Game Script!");
            
            if(transform.position != Vector3.zero)
                transform.position = Vector3.zero;
        }

        private IEnumerator CreatePlayer(Player playerPrefab, Vector3 spawnPoint)
        {
            yield return null;
            if(spawnPoint == Vector3.zero) 
                spawnPoint =  new FloatingPlayerSpawnPoint().Value();
            var player = Instantiate(playerPrefab, spawnPoint, Quaternion.identity);
            player.name = "Player";
        }

        private IEnumerator CreateCrowBar()
        {
            yield return null;
            var crowbar = new GameObject("Crowbar");
            Instantiate(crowbar, Vector3.zero, Quaternion.identity, transform);
            crowbar.AddComponent<Crowbar>();
            crowbar.AddComponent<DevicesInput>();
        }
    }
}
