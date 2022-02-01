using System.Collections.Generic;
using Common;
using GameObjectsScripts;
using Input;
using PlayerScripts;
using Services.Input;
using Services.Pools;
using UnityEditor;
using UnityEngine;

namespace Infrastructure
{
    public class Game
    {
        public static IInputService InputService;
        public static PlayerData PlayerData;
        public static GamePlayerData GamePlayerData;
        public Game()
        {
            RegisterInputService();
            PlayerData = CreatePlayerData();
            GamePlayerData = InitializeGamePlayerData(PlayerData);
        }
        
        private void RegisterInputService()
        {
            if (Application.isEditor)
                InputService = new KeyboardInputService();
            else
                InputService = new UiInputService();
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
