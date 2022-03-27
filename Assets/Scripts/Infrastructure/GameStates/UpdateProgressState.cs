using System;
using Common;
using Infrastructure.Factories;
using Infrastructure.Services;
using Services.PersistentProgress;
using Services.SaveLoad;

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
            UpdatePlayerProgress(Save);
        }
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

        private void Save()
        {
            _serviceLocator.Single<ISaveLoadService>().SaveProgress();
        }
       
    }
}