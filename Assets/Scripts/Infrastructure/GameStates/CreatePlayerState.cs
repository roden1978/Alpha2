using System;
using Common;
using PlayerScripts;
using UnityEngine;

namespace Infrastructure.GameStates
{
    public class CreatePlayerState : IPayloadState<StatesPayload>
    {
        private readonly GamesStateMachine _stateMachine;
        private const string PlayerPath = @"Prefabs/Player/Player";
        private const string CrosshairPath = @"Prefabs/Crosschair/Crosshair";
        public CreatePlayerState(GamesStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        public void Enter(StatesPayload statesPayload)
        {
            CreatePlayer(statesPayload, OnLoaded);
        }

        private static void CreatePlayer(StatesPayload statesPayload, Action<StatesPayload> onLoaded)
        {
            GameObject playerPrefab = Resources.Load<GameObject>(PlayerPath);
            GameObject crosshairPrefab = Resources.Load<GameObject>(CrosshairPath);
            
            Player player = UnityEngine.Object.Instantiate(playerPrefab).GetComponent<Player>();
            statesPayload.Player = player;
            
            Crosshair crosshair = UnityEngine.Object.Instantiate(crosshairPrefab).GetComponent<Crosshair>();
            Throw playerThrow = player.GetComponent<Throw>();
            
            playerThrow.PoolService = statesPayload.Pool;
            playerThrow.Crosshair = crosshair;
            
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
            _stateMachine.Enter<PositionPlayerState, StatesPayload>(statesPayload);
        }
        
    }
}