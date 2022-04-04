using System;
using Common;
using Infrastructure.Factories;
using Infrastructure.Services;
using Services.PersistentProgress;

namespace Infrastructure.GameStates
{
    public class LoadControlsPanelState : IState
    {
        private readonly ServiceLocator _serviceLocator;

        public LoadControlsPanelState(ServiceLocator serviceLocator)
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
            LoadControlsPanel();
        }
     
        private void LoadControlsPanel()
        {
            _serviceLocator.Single<IGameFactory>().CreateControlsPanel();
        }
    }
}