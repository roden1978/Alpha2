using System;
using Common;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.GameStates
{
    public class CreateHudState : IPayloadState<MediatorData>
    {
        private readonly GamesStateMachine _stateMachine;
        private const string Path = @"Prefabs/UI/HUD";

        public CreateHudState(GamesStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        public void Enter(MediatorData mediatorData)
        {
            CreateHud(mediatorData: mediatorData, OnLoaded);
        }

        public Type Update()
        {
            return null;
        }

        public void Exit()
        {
            
        }

        private static void CreateHud(MediatorData mediatorData, Action<MediatorData> onLoaded)
        {
            GameObject hudPrefab = Resources.Load<GameObject>(Path);
            Hud hud = Object.Instantiate(hudPrefab).GetComponent<Hud>();
            mediatorData.Hud = hud;
            onLoaded?.Invoke(mediatorData);
        }

        private void OnLoaded(MediatorData mediatorData)
        {
            _stateMachine.Enter<CreateMediatorState, MediatorData>(mediatorData);
        }
    }
}