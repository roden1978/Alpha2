using System;
using Common;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.GameStates
{
    public class LoadLevelState : IPayloadState<int>
    {
        private readonly SceneLoader _sceneLoader;
        private readonly GamesStateMachine _stateMachine;
        
        public LoadLevelState(GamesStateMachine stateMachine, SceneLoader sceneLoader)
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
            _stateMachine.Enter<CreatePlayerState, string>(@"Prefabs/Player/Player");
        }
        
    }
}