using System;
using Common;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.GameStates
{
    public class CreateMediatorState : IPayloadState<StatesPayload>
    {
        private const string Path = @"Prefabs/Common/Mediator";
        private readonly GamesStateMachine _stateMachine;

        public CreateMediatorState(GamesStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
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
            GameObject prefab = Resources.Load<GameObject>(Path);
            Mediator mediator = Object.Instantiate(prefab).GetComponent<Mediator>();
            mediator.InteractableObjectsCollector = statesPayload.InteractableObjectsCollector;
            mediator.Hud = statesPayload.Hud;
            mediator.Crowbar = statesPayload.Crowbar;
            mediator.ControlsPanel = statesPayload.ControlsPanel;
            onLoaded?.Invoke();
        }
    }
}