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
            RegisterPoolsService();
        }
        
        private void RegisterInputService()
        {
            if (Application.isEditor)
                InputService = new KeyboardInputService();
            else
                InputService = new UiInputService();
        }

        private void RegisterPoolsService()
        {
            GameObject go = new GameObject(typeof(Axe).ToString(), typeof(Axe));
            Axe axe = go.GetComponent<Axe>();
            List<IPool> pools = new List<IPool>
            {
                new Pool(axe, 10)
            };

            PoolService = new PoolService(pools);
            PoolService.Initialize();
        }
    }
}
