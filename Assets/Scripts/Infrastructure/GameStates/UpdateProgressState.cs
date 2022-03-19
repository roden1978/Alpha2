using System;
using Common;
using Infrastructure.Factories;
using Infrastructure.Services;
using PlayerScripts;
using Services.PersistentProgress;

namespace Infrastructure.GameStates
{
    public class UpdateProgressState : IState
    {
        private readonly ServiceLocator _serviceLocator;
        public UpdateProgressState(ServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }
        public Type Update()
        {
            return null;
        }

        public void Exit()
        {
        }
        public void Enter()
        {
            UpdatePlayerProgress();
        }
        private void UpdatePlayerProgress()
        {
            IPersistentProgressService persistentProgressService = _serviceLocator.Single<IPersistentProgressService>();
            IGameFactory gameFactory = _serviceLocator.Single<IGameFactory>();
            
            foreach (ISavedProgressReader readers in gameFactory.ProgressReaders)
            {
                readers.LoadProgress(persistentProgressService.PlayerProgress);
            }
        }
        
       
    }
}