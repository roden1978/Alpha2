using System;
using Common;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.GameStates
{
    public class CreateHudState : IPayloadState<string>
    {
        private readonly GamesStateMachine _stateMachine;

        public CreateHudState(GamesStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        public void Enter(string path)
        {
            CreateHud(path, OnLoaded);
        }

        public Type Update()
        {
            return null;
        }

        public void Exit()
        {
            
        }

        private static void CreateHud(string path, Action onLoaded)
        {
            GameObject hudPrefab = Resources.Load<GameObject>(path);
            Object.Instantiate(hudPrefab);
        }

        private static void OnLoaded()
        {
            
        }
    }
}