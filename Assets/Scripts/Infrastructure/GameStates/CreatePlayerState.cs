using System;
using Common;
using Infrastructure.Factories;
using Infrastructure.Services;
using PlayerScripts;
using UnityEngine;

namespace Infrastructure.GameStates
{
    public class CreatePlayerState : IPayloadState<StatesPayload>
    {
        private readonly GamesStateMachine _stateMachine;
        private readonly ServiceLocator _serviceLocator;


        public CreatePlayerState(GamesStateMachine stateMachine, ServiceLocator serviceLocator)
        {
            _stateMachine = stateMachine;
            _serviceLocator = serviceLocator;
        }

        private void CreatePlayer(StatesPayload statesPayload, Action<StatesPayload> onLoaded)
        {
            Player player = _serviceLocator.Single<IGameFactory>().CreatePlayer();
            statesPayload.Player = player;

            Crosshair crosshair = _serviceLocator.Single<IGameFactory>().CreateCrosshair();
            
            Throw playerThrow = player.GetComponent<Throw>();
            playerThrow.PoolService = statesPayload.Pool;
            playerThrow.Crosshair = crosshair;
            
            onLoaded?.Invoke(statesPayload);
        }

        public void Enter(StatesPayload statesPayload)
        {
            CreatePlayer(statesPayload, OnLoaded);
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
            _stateMachine.Enter<CreateCrowbarState, StatesPayload>(statesPayload);
        }
        
    }
}