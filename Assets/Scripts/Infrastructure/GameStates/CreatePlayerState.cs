using System;
using Common;
using UnityEngine;

namespace Infrastructure.GameStates
{
    public class CreatePlayerState : IPayloadState<string>
    {
        private readonly GamesStateMachine _stateMachine;

        public CreatePlayerState(GamesStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        public void Enter(string path)
        {
            GameObject player = CreatePlayer(path, OnLoaded);
        }

        private static GameObject CreatePlayer(string path, Action onLoaded)
        {
            GameObject playerPrefab = Resources.Load<GameObject>(path);
            onLoaded?.Invoke();
            return UnityEngine.Object.Instantiate(playerPrefab);
        }

        public Type Update()
        {
            return null;
        }

        public void Exit()
        {
            
        }

        private void OnLoaded()
        {
            _stateMachine.Enter<CreateHudState, string>(@"Prefabs/UI/HUD");
        }
        
    }
}