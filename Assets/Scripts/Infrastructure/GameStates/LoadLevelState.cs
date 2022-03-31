using System;
using System.Collections;
using Common;
using Infrastructure.Factories;
using Infrastructure.Services;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Infrastructure.GameStates
{
    public class LoadLevelState : IPayloadState<string>
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly Fader _fader;
        private readonly ServiceLocator _serviceLocator;
        private readonly GamesStateMachine _stateMachine;
        private const string FadeInAnimation = "FadeInAnimation";

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

        public Type Update()
        {
            return null;
        }

        public void Exit()
        {
            _fader.Hide();
        }

        private void OnLoaded()
        {
            Debug.Log("On loaded");
            _stateMachine.Enter<CreatePlayerState>();
        }

        private void LoadScene(string sceneName, Action onLoaded)
        {
            _sceneLoader.Load(sceneName, onLoaded);
        }
    }
}