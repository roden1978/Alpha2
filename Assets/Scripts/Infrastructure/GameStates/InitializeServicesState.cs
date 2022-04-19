using System;
using Common;
using Infrastructure.AssetManagement;
using Infrastructure.Factories;
using Infrastructure.Services;
using Services.PersistentProgress;
using Services.SaveLoad;
using Services.StaticData;

namespace Infrastructure.GameStates
{
    public class InitializeServicesState : IState
    {
        private readonly GamesStateMachine _stateMachine;
        private readonly ServiceLocator _serviceLocator;
        public InitializeServicesState(GamesStateMachine stateMachine, ServiceLocator serviceLocator)
        {
            _stateMachine = stateMachine;
            _serviceLocator = serviceLocator;
        }
        public void Enter() => 
            RegisterServices(NextState);
        public void Tick(){}
        public void Exit(){}
        private void RegisterServices(Action callback = null)
        {
            _serviceLocator.RegisterSingle<IGamesStateMachine>(_stateMachine);
            _serviceLocator.RegisterSingle<IAssetProvider>(new AssetProvider());
            _serviceLocator.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _serviceLocator.RegisterSingle<IStaticDataService>(new StaticDataService());
            
            _serviceLocator.RegisterSingle<IGameFactory>(
                new GameFactory(
                    _serviceLocator.Single<IAssetProvider>(), 
                    _serviceLocator.Single<IStaticDataService>()
                    )
            );
            _serviceLocator.RegisterSingle<ISaveLoadService>(new SaveLoadService(
                _serviceLocator.Single<IPersistentProgressService>(), 
                _serviceLocator.Single<IGameFactory>()
                )
            );

            RegisterStaticData(_serviceLocator.Single<IStaticDataService>());
            
            callback?.Invoke();
        }
        private void RegisterStaticData(IStaticDataService staticDataService)
        {
            staticDataService.LoadEnemies();
            staticDataService.LoadPickableObjects();
            staticDataService.LoadLevelStaticData();
            staticDataService.LoadSaveProgressPointStaticData();
        }
        private void NextState() => 
            _stateMachine.Enter<InitializeInputState>();
    }
}