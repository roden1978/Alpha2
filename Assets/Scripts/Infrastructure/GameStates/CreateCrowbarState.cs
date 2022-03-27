using System;
using Common;
using Infrastructure.Factories;
using Infrastructure.Services;
using PlayerScripts;
using Services.Pools;
using Services.StaticData;

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
        public void Enter()
        {
            CreateCrowbar(OnLoaded);
        }

        private void CreateCrowbar(Action onLoaded)
        {
            IGameFactory gameFactory = _serviceLocator.Single<IGameFactory>();
            IStaticDataService staticDataService = _serviceLocator.Single<IStaticDataService>();
            Player player = gameFactory.Player;
            Crowbar crowbar = gameFactory.CreateCrowbar();
            crowbar.Construct(player, staticDataService);
            onLoaded?.Invoke();
        }

        public Type Update()
        {
            return null;
        }

        public void Exit()
        {
           
        }

        private void OnLoaded()
        {
            _stateMachine.Enter<CreateHudState>();
        }
        
        
    }
}