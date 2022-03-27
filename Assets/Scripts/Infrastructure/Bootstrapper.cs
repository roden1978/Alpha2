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
            Fader fader = Instantiate(_fader).GetComponent<Fader>();
            _game = new Game(this, fader);
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            _game.GamesStateMachine.Enter<InitializeServicesState>();
        }
    }
}
