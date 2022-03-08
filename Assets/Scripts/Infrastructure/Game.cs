using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure
{
    public class Game
    {
        public readonly GamesStateMachine GamesStateMachine;

        public Game(ICoroutineRunner coroutineRunner)
        {
            SceneLoader sceneLoader = new SceneLoader(coroutineRunner);
            GamesStateMachine = new GamesStateMachine(sceneLoader, ServiceLocator.Container);
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
