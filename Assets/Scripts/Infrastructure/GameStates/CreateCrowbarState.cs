using System;
using Common;
using Infrastructure.Factories;
using Infrastructure.Services;
using PlayerScripts;
using Services.StaticData;

namespace Infrastructure.GameStates
{
    public class CreateCrowbarState : IPayloadState<StatesPayload>
    {
        private readonly GamesStateMachine _stateMachine;
        private readonly ServiceLocator _serviceLocator;

        public CreateCrowbarState(GamesStateMachine stateMachine, ServiceLocator serviceLocator)
        {
            _stateMachine = stateMachine;
            _serviceLocator = serviceLocator;
        }
        public void Enter(StatesPayload statesPayload)
        {
            CreateCrowbar(statesPayload, OnLoaded);
        }

        private void CreateCrowbar(StatesPayload statesPayload, Action<StatesPayload> onLoaded)
        {
            IGameFactory gameFactory = _serviceLocator.Single<IGameFactory>();
            IStaticDataService staticDataService = _serviceLocator.Single<IStaticDataService>();
            Crowbar crowbar = gameFactory.CreateCrowbar();
            crowbar.Construct(statesPayload.Player, staticDataService);
            statesPayload.InteractableObjectsCollector = statesPayload.Player.GetComponent<InteractableObjectsCollector>();
            statesPayload.Crowbar = crowbar;
            onLoaded?.Invoke(statesPayload);
        }

        public Type Update()
        {
            return null;
        }

        public void Exit()
        {
           
        }

        private void OnLoaded(StatesPayload statesPayload)
        {
            _stateMachine.Enter<CreateHudState, StatesPayload>(statesPayload);
        }
    }
}