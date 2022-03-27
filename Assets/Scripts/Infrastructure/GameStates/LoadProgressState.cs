using System;
using Common;
using Data;
using Infrastructure.Services;
using Services.PersistentProgress;
using Services.SaveLoad;
using UnityEngine.SceneManagement;

namespace Infrastructure.GameStates
{
    public class LoadProgressState : IState
    {
        private readonly GamesStateMachine _gamesStateMachine;
        private readonly ServiceLocator _serviceLocator;

        public LoadProgressState(GamesStateMachine gamesStateMachine, ServiceLocator serviceLocator)
        {
            _gamesStateMachine = gamesStateMachine;
            _serviceLocator = serviceLocator;
        }

        public void Enter() => LoadProgress(NextState);

        private void NextState()
        {
            _gamesStateMachine.Enter<InitializeInputState>();
        }

        private void LoadProgress(Action callback = null)
        {
            ISaveLoadService saveLoadService = _serviceLocator.Single<ISaveLoadService>();
            IPersistentProgressService persistentProgressService = _serviceLocator.Single<IPersistentProgressService>();
            
            persistentProgressService.PlayerProgress = saveLoadService.LoadProgress() ?? CreatePlayerProgress();
            
            callback?.Invoke();
        }

        private PlayerProgress CreatePlayerProgress()
        {
            string sceneName = SceneManager.GetActiveScene().name;
            PlayerProgress playerProgress = new PlayerProgress(sceneName);

            playerProgress.PlayerState.CurrentHealth = playerProgress.StaticPlayerData.Health;
            playerProgress.PlayerState.CurrentCrystalsAmount = playerProgress.StaticPlayerData.CrystalsAmount;
            playerProgress.PlayerState.CurrentLivesAmount = playerProgress.StaticPlayerData.StartLivesAmount;
            playerProgress.PlayerState.CurrentFruitScoresAmount = playerProgress.StaticPlayerData.FruitScoresAmount;
            playerProgress.PlayerState.MaxHealth = playerProgress.StaticPlayerData.MaxHealth;
            playerProgress.PlayerState.MaxBonusLivesCount = playerProgress.StaticPlayerData.MaxBonusLivesCount;
            playerProgress.PlayerState.StartLivesAmount = playerProgress.StaticPlayerData.StartLivesAmount;
           
            return playerProgress;
        }

        public Type Update()
        {
            return null;
        }

        public void Exit()
        {
            //throw new NotImplementedException();
        }
    }
}