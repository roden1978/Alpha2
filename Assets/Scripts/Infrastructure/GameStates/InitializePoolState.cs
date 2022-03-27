using System;
using Common;
using Infrastructure.Factories;
using Infrastructure.Services;

namespace Infrastructure.GameStates
{
    public class InitializePoolState : IState
    {
        private readonly GamesStateMachine _stateMachine;
        private readonly ServiceLocator _serviceLocator;
        public InitializePoolState(GamesStateMachine stateMachine, ServiceLocator serviceLocator)
        {
            _stateMachine = stateMachine;
            _serviceLocator = serviceLocator;
        }

        public void Enter()
        {
            InitializePool(OnLoaded);
        }

        public Type Update()
        {
            return null;
        }

        public void Exit()
        {
            
        }

        private void InitializePool(Action onLoaded)
        {
            _serviceLocator.Single<IGameFactory>().CreatePool();            
            onLoaded?.Invoke();
        }

        private void OnLoaded()
        {
            _stateMachine.Enter<CreatePlayerState>();
            
        }
    }
}