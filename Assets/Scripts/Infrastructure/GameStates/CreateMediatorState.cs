using System;
using Common;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.GameStates
{
    public class CreateMediatorState : IPayloadState<MediatorData>
    {
        private const string Path = @"Prefabs/Common/Mediator";
        private readonly GamesStateMachine _stateMachine;

        public CreateMediatorState(GamesStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter(MediatorData mediatorData)
        {
            CreateMediator(mediatorData, OnLoaded);
        }

        private void OnLoaded()
        {
            
        }

        public Type Update()
        {
            throw new NotImplementedException();
        }

        public void Exit()
        {
            throw new NotImplementedException();
        }

        private void CreateMediator(MediatorData mediatorData, Action onLoaded)
        {
            GameObject prefab = Resources.Load<GameObject>(Path);
            Mediator mediator = Object.Instantiate(prefab).GetComponent<Mediator>();
            mediator.InteractableObjectsCollector = mediatorData.InteractableObjectsCollector;
            mediator.Hud = mediatorData.Hud;
            mediator.Crowbar = mediatorData.Crowbar;
            onLoaded?.Invoke();
        }
    }
}