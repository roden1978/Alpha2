using Infrastructure.Services;
using PlayerScripts;
using Services.Input;
using UnityEngine;

namespace Infrastructure
{
    public class Game
    {
        public static PlayerData PlayerData;
        public static GamePlayerData GamePlayerData;
        public static SceneLoader SceneLoader;
        public GamesStateMachine GamesStateMachine;
       

        public Game(ICoroutineRunner coroutineRunner)
        {
            SceneLoader = new SceneLoader(coroutineRunner);
            GamesStateMachine = new GamesStateMachine(SceneLoader, ServiceLocator.Container);
            PlayerData = CreatePlayerData();
            GamePlayerData = InitializeGamePlayerData(PlayerData);
        }
        
        

        private PlayerData CreatePlayerData()
        {
            return new PlayerData();
        }

        private GamePlayerData InitializeGamePlayerData(PlayerData playerData)
        {
            GamePlayerData gamePlayerData = new GamePlayerData(playerData);
            gamePlayerData.InitializeGamePlayerData();
            return gamePlayerData;
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
