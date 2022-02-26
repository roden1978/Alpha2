using System;
using Common;
using Infrastructure.Factories;

namespace Infrastructure.GameStates
{
    public class LoadControlsPanelState : IPayloadState<StatesPayload>
    {
        private readonly GamesStateMachine _stateMachine;
        private IGameFactory _gameFactory;

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
            statesPayload.ControlsPanel = _gameFactory.CreateControlsPanel();
            onLoaded?.Invoke(statesPayload);
        }
    }
}