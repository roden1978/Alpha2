using Infrastructure.Services;
using PlayerScripts;
using UnityEngine;

namespace Infrastructure
{
    public class Game
    {
        public static StaticPlayerData StaticPlayerData;
        public readonly GamesStateMachine GamesStateMachine;

        public Game(ICoroutineRunner coroutineRunner)
        {
            SceneLoader sceneLoader = new SceneLoader(coroutineRunner);
            GamesStateMachine = new GamesStateMachine(sceneLoader, ServiceLocator.Container);
            StaticPlayerData = new StaticPlayerData();
        }
       
        public static void Pause()
        {
            Time.timeScale = 0;
        }

        public static void Resume()
        {
            Time.timeScale = 1;
        }

    }
}
