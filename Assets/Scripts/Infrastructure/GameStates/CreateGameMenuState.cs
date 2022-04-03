using System;
using Common;
using Infrastructure.Factories;
using Infrastructure.Services;

namespace Infrastructure.GameStates
{
    public class CreateGameMenuState : IState
    {
        private readonly GamesStateMachine _stateMachine;
        private readonly ServiceLocator _serviceLocator;

        public CreateGameMenuState(GamesStateMachine stateMachine, ServiceLocator serviceLocator)
        {
            _stateMachine = stateMachine;
            _serviceLocator = serviceLocator;
        }
        public void Enter()
        {
            CreateGameMenu(OnLoaded);
        }

        public Type Update()
        {
            return null;
        }

        public void Exit()
        {
            
        }

        private void CreateGameMenu(Action onLoaded)
        {
            _serviceLocator.Single<IGameFactory>().CreateGameMenu();
            onLoaded?.Invoke();
        }

        private void OnLoaded()
        {
            
        }
    }
}