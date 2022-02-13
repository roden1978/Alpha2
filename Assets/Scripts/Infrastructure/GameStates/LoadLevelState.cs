using System;
using Common;

namespace Infrastructure.GameStates
{
    public class LoadLevelState : IPayloadState<int>
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly GamesStateMachine _stateMachine;
        
        public LoadLevelState(GamesStateMachine stateMachine, ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            _stateMachine = stateMachine;
        }
        public void Enter(int sceneIndex)
        {
            _sceneLoader.Load(sceneIndex, OnLoaded);
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
            _stateMachine.Enter<InitializePoolState, string>(@"Prefabs/Common/PlayersPools");
        }
        
    }
}