using System;
using Cinemachine;
using Common;
using PlayerScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class ScenesPrincipal : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Fader _fader;
        private AsyncOperation _operation;
        private PlayerSpawnPoint _spawnPoint;
        [SerializeField] private CinemachineVirtualCamera _camera;
        private int _currentSceneIndex;

        public Action UpdateHud;

        private void Start()
        {
            _player.Death += OnPlayerDeath;
        }

        private void OnPlayerDeath()
        {
            RestartCurrentLevel();
        }

        private void RestartCurrentLevel()
        {
            UnloadLevel(Game.GamePlayerData.CurrentScene);
            LoadLevel(Game.GamePlayerData.CurrentScene);
            ResetProgress();
            UpdateHud?.Invoke();
        }

        public AsyncOperation LoadLevel(int index)
        {
            _currentSceneIndex = index;
            _operation = SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
            _operation.completed += LevelStart;
            return _operation;
        }

        public void UnloadLevel(int index)
        {
            _fader.FadeIn();
            SceneManager.UnloadSceneAsync(index);
        }

        public void LevelStart(AsyncOperation operation)
        {
            if(operation.isDone)
            {
                _fader.FadeOut();
                Game.GamePlayerData.CurrentScene = _currentSceneIndex;
            }
            _spawnPoint = FindObjectOfType<PlayerSpawnPoint>();
            Transform playerTransform = _player.transform;
            playerTransform.position = _spawnPoint.transform.position;
            _camera.transform.position = playerTransform.position;
            _player.gameObject.SetActive(true);
        }
        
        private void ResetProgress()
        {
            Game.GamePlayerData.CurrentHealth = Game.PlayerData.Health;
            Game.GamePlayerData.CurrentFruitScoresAmount = Game.PlayerData.FruitScoresAmount;
            Game.GamePlayerData.CurrentCrystalsAmount = Game.PlayerData.CrystalsAmount;
        }

       
    }
}
