using System;
using Common;
using Infrastructure.Factories;
using Infrastructure.Services;

namespace Infrastructure.GameStates
{
    public class LoadLevelState : IPayloadState<string>
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly Fader _fader;
        private readonly ServiceLocator _serviceLocator;
        private readonly GamesStateMachine _stateMachine;
        public LoadLevelState(GamesStateMachine stateMachine, ISceneLoader sceneLoader, Fader fader,
            ServiceLocator serviceLocator)
        {
            _sceneLoader = sceneLoader;
            _fader = fader;
            _serviceLocator = serviceLocator;
            _stateMachine = stateMachine;
        }
        public void Enter(string sceneName)
        {
            _serviceLocator.Single<IGameFactory>().CleanUp();
            _fader.Show();
            LoadScene(sceneName, OnLoaded);
        }
        public void Update(){}
        public void Exit(){}
        private void OnLoaded() => 
            _stateMachine.Enter<CreatePlayerState>();
        private void LoadScene(string sceneName, Action onLoaded) => 
            _sceneLoader.Load(sceneName, onLoaded);
    }
}