using System;
using Common;
using PlayerScripts;
using Services.Pools;
using UnityEngine;

namespace Infrastructure.GameStates
{
    public class CreatePlayerState : IPayloadState<PoolService>
    {
        private readonly GamesStateMachine _stateMachine;
        private const string PlayerPath = @"Prefabs/Player/Player";
        private const string CrosshairPath = @"Prefabs/Crosschair/Crosshair";
        public CreatePlayerState(GamesStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        public void Enter(PoolService pool)
        {
            CreatePlayer(pool, OnLoaded);
        }

        private static void CreatePlayer(PoolService poolService, Action<Player> onLoaded)
        {
            GameObject playerPrefab = Resources.Load<GameObject>(PlayerPath);
            GameObject crosshairPrefab = Resources.Load<GameObject>(CrosshairPath);
            Player player = UnityEngine.Object.Instantiate(playerPrefab).GetComponent<Player>();
            Crosshair crosshair = UnityEngine.Object.Instantiate(crosshairPrefab).GetComponent<Crosshair>();
            Throw playerThrow = player.GetComponent<Throw>();
            playerThrow.PoolService = poolService;
            playerThrow.Crosshair = crosshair;
            onLoaded?.Invoke(player);
        }

        public Type Update()
        {
            return null;
        }

        public void Exit()
        {
            
        }

        private void OnLoaded(Player player)
        {
            _stateMachine.Enter<PositionPlayerState, Player>(player);
        }
        
    }
}