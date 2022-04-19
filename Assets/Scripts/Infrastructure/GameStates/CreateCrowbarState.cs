using System;
using Common;
using Infrastructure.Factories;
using Infrastructure.Services;

namespace Infrastructure.GameStates
{
    public class CreateCrowbarState : IState
    {
        private readonly GamesStateMachine _stateMachine;
        private readonly ServiceLocator _serviceLocator;
        public CreateCrowbarState(GamesStateMachine stateMachine, ServiceLocator serviceLocator)
        {
            _stateMachine = stateMachine;
            _serviceLocator = serviceLocator;
        }
        public void Enter() => 
            CreateCrowbar(OnLoaded);
        private void CreateCrowbar(Action onLoaded)
        {
            _serviceLocator.Single<IGameFactory>().CreateCrowbar();
            onLoaded?.Invoke();
        }
        public void Tick(){}
        public void Exit(){}
        private void OnLoaded() => 
            _stateMachine.Enter<CreateHudState>();
    }
}