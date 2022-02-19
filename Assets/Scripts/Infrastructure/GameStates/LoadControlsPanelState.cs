using System;
using Common;
using UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.GameStates
{
    public class LoadControlsPanelState : IPayloadState<StatesPayload>
    {
        private const string Path = @"Prefabs/UI/ControlsPanel";
        private readonly GamesStateMachine _stateMachine;


        public LoadControlsPanelState(GamesStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public Type Update()
        {
            return null;
        }

        public void Exit()
        {
        }

        public void Enter(StatesPayload statesPayload)
        {
            LoadControlsPanel(statesPayload, OnLoaded);
        }

        private void OnLoaded(StatesPayload statesPayload)
        {
            _stateMachine.Enter<LoadLevelState, StatesPayload>(statesPayload);
        }

        private void LoadControlsPanel(StatesPayload statesPayload, Action<StatesPayload> onLoaded)
        {
            GameObject prefab = Resources.Load<GameObject>(Path);
            ControlsPanel controlsPanel = Object.Instantiate(prefab).GetComponent<ControlsPanel>();
            statesPayload.ControlsPanel = controlsPanel;
            onLoaded?.Invoke(statesPayload);
        }
    }
}