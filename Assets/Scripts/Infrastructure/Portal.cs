using PlayerScripts;
using UnityEngine;

namespace Infrastructure
{
    public class Portal : MonoBehaviour, ICoroutineRunner
    {
        private ISceneLoader _sceneLoader;
        private const int PlayerLayer = 1 << 6;
        private void Awake()
        {
            _sceneLoader = new SceneLoader(this);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.layer == PlayerLayer)
                Transit();
        }
        
        private void Transit()
        {
            //player.gameObject.SetActive(false);
            _sceneLoader.UnLoad(Game.GamePlayerData.CurrentScene);
            int newSceneIndex = Game.GamePlayerData.CurrentScene + 1;
            _sceneLoader.Load(newSceneIndex);
            Game.GamePlayerData.CurrentScene = newSceneIndex;
        }
    }
}
