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
            _game.GamesStateMachine.Enter<InputInitializeState>();
            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            _game.GamesStateMachine.Update();
        }
    }
}
