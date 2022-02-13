using System;
using Common;
using Services.Pools;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.GameStates
{
    public class InitializePoolState : IPayloadState<string>
    {
        private readonly GamesStateMachine _stateMachine;

        public InitializePoolState(GamesStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter(string payload)
        {
            InitializePool(payload, OnLoaded);
        }

        public Type Update()
        {
            return null;
        }

        public void Exit()
        {
            
        }

        private void InitializePool(string payload, Action<PoolService> onLoaded)
        {
            GameObject prefab = Resources.Load<GameObject>(payload);
            PoolService pool = Object.Instantiate(prefab).GetComponent<PoolService>();
            onLoaded?.Invoke(pool);
        }

        private void OnLoaded(PoolService poolService)
        {
            _stateMachine.Enter<CreatePlayerState, PoolService>(poolService);
            
        }
    }
}