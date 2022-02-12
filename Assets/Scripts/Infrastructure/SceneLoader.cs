using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class SceneLoader
    {
        //[SerializeField] private Player _player;
        //[SerializeField] private Fader _fader;
        private AsyncOperation _wait;
        //private PlayerSpawnPoint _spawnPoint;

        public Action UpdateHud;
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Load(int index, Action callback = null) =>
            _coroutineRunner.StartCoroutine(LoadLevel(index, callback));

        /*private void Start()
        {
            _player.Death += OnPlayerDeath;
        }*/

        /*private void OnPlayerDeath()
        {
            RestartCurrentLevel();
        }*/

        /*private void RestartCurrentLevel()
        {
            UnloadLevel(Game.GamePlayerData.CurrentScene);
            LoadLevel(Game.GamePlayerData.CurrentScene);
            ResetProgress();
            UpdateHud?.Invoke();
        }*/

        private IEnumerator LoadLevel(int index, Action callback = null)
        {
            _wait = SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
            while (!_wait.isDone)
            {
                yield return null;
            }
            callback?.Invoke();
        }

       
        /*public void LevelStart(AsyncOperation operation)
        {
            if(operation.isDone)
            {
                _fader.FadeOut();
                Game.GamePlayerData.CurrentScene = _currentSceneIndex;
            }
            _spawnPoint = FindObjectOfType<PlayerSpawnPoint>();
            Transform playerTransform = _player.transform;
            playerTransform.position = _spawnPoint.transform.position;
            _player.gameObject.SetActive(true);
        }*/
        
        /*private void ResetProgress()
        {
            Game.GamePlayerData.CurrentHealth = Game.PlayerData.Health;
            Game.GamePlayerData.CurrentFruitScoresAmount = Game.PlayerData.FruitScoresAmount;
            Game.GamePlayerData.CurrentCrystalsAmount = Game.PlayerData.CrystalsAmount;
        }*/

       
    }

    public class SceneUnloader
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private AsyncOperation _wait;
        public SceneUnloader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void UnLoad(int index, Action callback = null) =>
            _coroutineRunner.StartCoroutine(UnLoadLevel(index));
        

        private IEnumerator UnLoadLevel(int index, Action callback = null)
        {
            _wait = SceneManager.UnloadSceneAsync(index);
            while (!_wait.isDone)
            {
                yield return null;
            }
            callback?.Invoke();
        }
    }
}
