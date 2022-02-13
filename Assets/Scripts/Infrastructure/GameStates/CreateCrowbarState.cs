using System;
using Common;
using PlayerScripts;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.GameStates
{
    public class CreateCrowbarState : IPayloadState<Player>
    {
        private const string Path = @"Prefabs/Common/Crowbar";
        private readonly GamesStateMachine _stateMachine;

        public CreateCrowbarState(GamesStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        public void Enter(Player player)
        {
            CreateCrowbar(player, OnLoaded);
        }

        private void CreateCrowbar(Player player, Action onLoaded)
        {
            GameObject prefab = Resources.Load<GameObject>(Path);
            Crowbar crowbar = Object.Instantiate(prefab).GetComponent<Crowbar>();
            crowbar.Player = player;
            onLoaded?.Invoke();
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