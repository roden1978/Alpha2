using System.Collections.Generic;
using Common;
using GameObjectsScripts;
using Input;
using Services.Input;
using Services.Pools;
using UnityEngine;

namespace Infrastructure
{
    public class Game
    {
        public static IInputService InputService;
        public static IPoolService PoolService;
        public Game()
        {
            RegisterInputService();
        }
        
        private void RegisterInputService()
        {
            if (Application.isEditor)
                InputService = new KeyboardInputService();
            else
                InputService = new UiInputService();
        }

    }
}
