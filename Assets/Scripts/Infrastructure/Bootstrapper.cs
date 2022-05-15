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
            fader.gameObject.SetActive(false);
            _game = new Game(this, fader);
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            _game.GamesStateMachine.Enter<InitializeServicesState>();
        }

        public void LoadLevelState()
        {
            _game.GamesStateMachine.Enter<LoadProgressState>();
        }
    }
}
