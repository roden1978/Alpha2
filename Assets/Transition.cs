using Infrastructure;
using PlayerScripts;
using UnityEngine;

public class Transition : MonoBehaviour
{
    /*[SerializeField] private SceneLoader _principal;
    [SerializeField] private Player _player;
    private AsyncOperation _operation;
    private void Start()
    {
        _player.Transition += Transit;
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
