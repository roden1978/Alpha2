using System;
using System.Collections.Generic;
using Common;
using Infrastructure.GameStates;
using Infrastructure.Services;

namespace Infrastructure
{
    public class GamesStateMachine : IGamesStateMachine
    {
        private readonly Dictionary<Type, IUpdateableState> _states;
        private IUpdateableState _activeState;

        public GamesStateMachine(ISceneLoader sceneLoader, ServiceLocator serviceLocator, Fader fader)
        {
            _states = new Dictionary<Type, IUpdateableState>
            {
                [typeof(InitializeServicesState)] = new InitializeServicesState(this, serviceLocator),
                [typeof(InitializeInputState)] = new InitializeInputState(this, serviceLocator),
                [typeof(LoadControlsPanelState)] = new LoadControlsPanelState(this, serviceLocator),
                [typeof(LoadProgressState)] = new LoadProgressState(this, serviceLocator),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, fader, serviceLocator),
                [typeof(CreatePlayerState)] = new CreatePlayerState(this, serviceLocator),
                [typeof(CreateCrowbarState)] = new CreateCrowbarState(this, serviceLocator),
                [typeof(CreateHudState)] = new CreateHudState(this, serviceLocator),
                [typeof(CreateMediatorState)] = new CreateMediatorState(this, serviceLocator),
                [typeof(SpawnEntitiesState)] = new SpawnEntitiesState(this, serviceLocator),
                [typeof(UpdateProgressState)] = new UpdateProgressState(this,serviceLocator, fader),
                [typeof(CreateGameMenuState)] = new CreateGameMenuState(this, serviceLocator)
            };
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