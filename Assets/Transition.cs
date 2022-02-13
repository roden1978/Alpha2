using Infrastructure;
using PlayerScripts;
using UnityEngine;

public class Transition : MonoBehaviour
{
    /*private ISceneLoader _sceneLoader;
    [SerializeField] private Player _player;
    private AsyncOperation _operation;
    private void Start()
    {
        _player.Transition += Transit;
        //_sceneLoader = new SceneLoader()
    }

    private void Transit()
    {
        _player.gameObject.SetActive(false);
        _principal.UnloadLevel(Game.GamePlayerData.CurrentScene);
        int newSceneIndex = Game.GamePlayerData.CurrentScene + 1;
        _operation = _principal.LoadLevel(newSceneIndex);
        Game.GamePlayerData.CurrentScene = newSceneIndex;
        _principal.LevelStart(_operation);
    }*/
}
