using System;
using Common;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.GameStates
{
    public class CreateHudState : IPayloadState<StatesPayload>
    {
        private readonly GamesStateMachine _stateMachine;
        private const string Path = @"Prefabs/UI/HUD";

        public CreateHudState(GamesStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        public void Enter(StatesPayload statesPayload)
        {
            CreateHud(statesPayload: statesPayload, OnLoaded);
        }

        public Type Update()
        {
            return null;
        }

        public void Exit()
        {
            
        }

        private static void CreateHud(StatesPayload statesPayload, Action<StatesPayload> onLoaded)
        {
            GameObject hudPrefab = Resources.Load<GameObject>(Path);
            Hud hud = Object.Instantiate(hudPrefab).GetComponent<Hud>();
            statesPayload.Hud = hud;
            onLoaded?.Invoke(statesPayload);
        }

        private void OnLoaded(StatesPayload statesPayload)
        {
            _stateMachine.Enter<CreateMediatorState, StatesPayload>(statesPayload);
        }
    }
}