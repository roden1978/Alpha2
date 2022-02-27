using System;
using Common;

namespace Infrastructure.GameStates
{
    public class LoadLevelState : IPayloadState<StatesPayload>
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly GamesStateMachine _stateMachine;
        private StatesPayload _statesPayload;

        public LoadLevelState(GamesStateMachine stateMachine, ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            _stateMachine = stateMachine;
        }
        public void Enter(StatesPayload statesPayload)
        {
            _statesPayload = statesPayload;
            LoadScene(statesPayload.CurrentSceneIndex, OnLoaded);
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
            _stateMachine.Enter<InitializePoolState, StatesPayload>(_statesPayload);
        }

        private void LoadScene(int sceneIndex, Action onLoaded)
        {
            _sceneLoader.Load(sceneIndex, onLoaded);
        }
        
    }
}