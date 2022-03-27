using System;
using Common;

namespace Infrastructure.GameStates
{
    public class LoadLevelState : IPayloadState<string>
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly Fader _fader;
        private readonly GamesStateMachine _stateMachine;

        public LoadLevelState(GamesStateMachine stateMachine, ISceneLoader sceneLoader, Fader fader)
        {
            _sceneLoader = sceneLoader;
            _fader = fader;
            _stateMachine = stateMachine;
        }
        public void Enter(string sceneName)
        {
            _fader.FadeIn();
            LoadScene(sceneName, OnLoaded);
        }

        public Type Update()
        {
            return null;
        }

        public void Exit()
        {
            _fader.FadeOut();
        }
        
        private void OnLoaded()
        {
            _stateMachine.Enter<CreatePlayerState>();
        }
        private void LoadScene(string sceneName, Action onLoaded)
        {
            _sceneLoader.Load(sceneName, onLoaded);
        }
        
    }
}