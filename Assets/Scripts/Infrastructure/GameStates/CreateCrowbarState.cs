using System;
using Common;
using PlayerScripts;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.GameStates
{
    public class CreateCrowbarState : IPayloadState<StatesPayload>
    {
        private const string Path = @"Prefabs/Common/Crowbar";
        private readonly GamesStateMachine _stateMachine;
        
        public CreateCrowbarState(GamesStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        
        }
        public void Enter(StatesPayload statesPayload)
        {
            CreateCrowbar(statesPayload, OnLoaded);
        }

        private void CreateCrowbar(StatesPayload statesPayload, Action<StatesPayload> onLoaded)
        {
            GameObject prefab = Resources.Load<GameObject>(Path);
            Crowbar crowbar = Object.Instantiate(prefab).GetComponent<Crowbar>();
            crowbar.Player = statesPayload.Player;
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