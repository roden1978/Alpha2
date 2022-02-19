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
        private MediatorData _mediatorData;
        public CreateCrowbarState(GamesStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _mediatorData = new MediatorData();

        }
        public void Enter(Player player)
        {
            CreateCrowbar(player, OnLoaded);
        }

        private void CreateCrowbar(Player player, Action<MediatorData> onLoaded)
        {
            GameObject prefab = Resources.Load<GameObject>(Path);
            Crowbar crowbar = Object.Instantiate(prefab).GetComponent<Crowbar>();
            crowbar.Player = player;
            _mediatorData.InteractableObjectsCollector = player.GetComponent<InteractableObjectsCollector>();
            _mediatorData.Crowbar = crowbar;
            onLoaded?.Invoke(_mediatorData);
        }

        public Type Update()
        {
            return null;
        }

        public void Exit()
        {
           
        }

        private void OnLoaded(MediatorData mediatorData)
        {
            _stateMachine.Enter<CreateHudState, MediatorData>(mediatorData);
        }
    }
}