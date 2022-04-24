using System;
using Common;
using Infrastructure.Factories;
using Infrastructure.Services;

namespace Infrastructure.GameStates
{
    public class CreateGameMenuState : IState
    {
        private readonly ServiceLocator _serviceLocator;
        public CreateGameMenuState(ServiceLocator serviceLocator) => 
            _serviceLocator = serviceLocator;
        public void Enter() =>
            CreateGameMenu();
        public void Update(){}
        public void Exit(){}
        private void CreateGameMenu()
        {
            _serviceLocator.Single<IGameFactory>().CreateGameMenu();
        }
    }
}