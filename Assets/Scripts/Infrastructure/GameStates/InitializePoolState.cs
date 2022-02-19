using System;
using Common;
using Services.Pools;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.GameStates
{
    public class InitializePoolState : IPayloadState<StatesPayload>
    {
        private readonly GamesStateMachine _stateMachine;
        private const string Path = @"Prefabs/Common/PlayersPools";
        public InitializePoolState(GamesStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter(StatesPayload statesPayload)
        {
            InitializePool(statesPayload, OnLoaded);
        }

        public Type Update()
        {
            return null;
        }

        public void Exit()
        {
            
        }

        private void InitializePool(StatesPayload statesPayload, Action<StatesPayload> onLoaded)
        {
            GameObject prefab = Resources.Load<GameObject>(Path);
            PoolService pool = Object.Instantiate(prefab).GetComponent<PoolService>();
            statesPayload.Pool = pool;
            onLoaded?.Invoke(statesPayload);
        }

        private void OnLoaded(StatesPayload statesPayload)
        {
            _stateMachine.Enter<CreatePlayerState, StatesPayload>(statesPayload);
            
        }
    }
}