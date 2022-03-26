using Common;
using Infrastructure.GameStates;
using UnityEngine;

namespace Infrastructure
{
    public class Bootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private Fader _fader;
        private Game _game;

        private void Awake()
        {
            //Start fader in Game class (this, _fader)
            _game = new Game(this);
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            _game.GamesStateMachine.Enter<InitializeServicesState>();
        }
    }
}
