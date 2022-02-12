using System;
using System.Collections.Generic;
using Common;
using Infrastructure.GameStates;

namespace Infrastructure
{
    public class GamesStateMachine
    {
        private readonly Dictionary<Type, IUpdateableState> _states;
        private IUpdateableState _activeState;

        public GamesStateMachine(SceneLoader sceneLoader)
        {
            _states = new Dictionary<Type, IUpdateableState>
            {
                [typeof(InputInitializeState)] = new InputInitializeState(this),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader),
                [typeof(CreatePlayerState)] = new CreatePlayerState(this),
                [typeof(CreateHudState)] = new CreateHudState(this),
            };
        }

        public void Update()
        {
            _activeState.Update();
        }
        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IUpdateableState
        {
            _activeState?.Exit();
            TState state = State<TState>();
            _activeState = state;
            return state;
        }

        private TState State<TState>() where TState : class, IUpdateableState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}