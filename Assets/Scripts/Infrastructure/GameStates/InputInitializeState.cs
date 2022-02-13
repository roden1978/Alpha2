using System;
using Common;
using Input;
using Services.Input;
using UnityEngine;

namespace Infrastructure.GameStates
{
    public class InputInitializeState : IState
    {
        private readonly GamesStateMachine _stateMachine;
        public InputInitializeState(GamesStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        public void Enter()
        {
            RegisterServices(NextState);
        }

        public Type Update()
        {
            return null;
        }

        public void Exit()
        {
            
        }

        private void RegisterServices(Action callback = null)
        {
            Game.InputService = RegisterInputService();
            callback?.Invoke();
        }
        private IInputService RegisterInputService()
        {
            if (Application.isEditor)
                return new KeyboardInputService();

            Game.Mobile = true;
            return new UiInputService();
        }

        private void NextState()
        {
            _stateMachine.Enter<LoadLevelState, int>(1);
        }
    }
}