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
       public AsyncOperation LoadLevel(int index)
        {
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
            }
            PlayerSpawnPoint point = FindObjectOfType<PlayerSpawnPoint>();
            _player.transform.position = point.transform.position;
            _player.gameObject.SetActive(true);
        }
    }
}
