using System;
using Common;
using PlayerScripts;
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
            CreatePlayer(path, OnLoaded);
        }

        private static void CreatePlayer(string path, Action<Player> onLoaded)
        {
            GameObject playerPrefab = Resources.Load<GameObject>(path);
            Player player = UnityEngine.Object.Instantiate(playerPrefab).GetComponent<Player>();
            //player.gameObject.SetActive(false);
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