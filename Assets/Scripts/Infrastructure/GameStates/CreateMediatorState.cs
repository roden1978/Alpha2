using System;
using Common;
using Infrastructure.Factories;
using Infrastructure.Services;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.GameStates
{
    public class CreateMediatorState : IPayloadState<StatesPayload>
    {
        
        private readonly GamesStateMachine _stateMachine;
        private readonly ServiceLocator _serviceLocator;

        public CreateMediatorState(GamesStateMachine stateMachine, ServiceLocator serviceLocator)
        {
            _stateMachine = stateMachine;
            _serviceLocator = serviceLocator;
        }

        public void Enter(StatesPayload statesPayload)
        {
            CreateMediator(statesPayload, OnLoaded);
        }

        private void OnLoaded()
        {
            _stateMachine.Enter<UpdateProgressState>();
        }

        public Type Update()
        {
            return null;
        }

        public void Exit()
        {
            //throw new NotImplementedException();
        }

        private void CreateMediator(StatesPayload statesPayload, Action onLoaded)
        {
            Mediator mediator = _serviceLocator.Single<IGameFactory>().CreateMediator();
            mediator.InteractableObjectsCollector = statesPayload.InteractableObjectsCollector;
            mediator.Hud = statesPayload.Hud;
            mediator.Player = statesPayload.Player;
            mediator.ControlsPanel = statesPayload.ControlsPanel;
            onLoaded?.Invoke();
        }
    }
}