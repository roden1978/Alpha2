using System;
using Common;
using Infrastructure.Factories;
using Infrastructure.Services;
using Services.PersistentProgress;
using UI;
using UnityEngine;

namespace Infrastructure.GameStates
{
    public class UpdateProgressState : IState
    {
        private readonly GamesStateMachine _stateMachine;
        private readonly ServiceLocator _serviceLocator;
        private readonly Fader _fader;
        public UpdateProgressState(GamesStateMachine stateMachine, ServiceLocator serviceLocator, Fader fader)
        {
            _stateMachine = stateMachine;
            _serviceLocator = serviceLocator;
            _fader = fader;
        }
        public void Tick(){}
        public void Exit(){}
        public void Enter()
        {
            UpdatePlayerProgress(HideFader);
            ControlsPanel controlPanel = _serviceLocator.Single<IGameFactory>().ControlsPanel;
            if(controlPanel != null)
                ResetJoystick(controlPanel);
            LoadGameMenu();
        }
        private void LoadGameMenu() => 
            _stateMachine.Enter<CreateGameMenuState>();
        private static void ResetJoystick(ControlsPanel controlPanel)
        {
            controlPanel.gameObject.SetActive(true);
            RectTransform rectTransform = (RectTransform)controlPanel.OnScreenStick.transform;
            rectTransform.anchoredPosition = controlPanel.JoystickStartPosition;
        }
        private void HideFader() => 
            _fader.Hide();
        private void UpdatePlayerProgress(Action callback)
        {
            IPersistentProgressService persistentProgressService = _serviceLocator.Single<IPersistentProgressService>();
            IGameFactory gameFactory = _serviceLocator.Single<IGameFactory>();
            
            foreach (ISavedProgressReader readers in gameFactory.ProgressReaders)
            {
                readers.LoadProgress(persistentProgressService.PlayerProgress);
            }
            callback?.Invoke();
        }
    }
}