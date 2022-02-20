using System;
using Infrastructure.GameStates;
using UnityEngine;

namespace Infrastructure
{
    public class Bootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private Game _game;

        private void Awake()
        {
            _game = new Game(this);
            DontDestroyOnLoad(this);
        }

        public void StartGame()
        {
            _game.GamesStateMachine.Enter<InputInitializeState>();
        }
    }
}
