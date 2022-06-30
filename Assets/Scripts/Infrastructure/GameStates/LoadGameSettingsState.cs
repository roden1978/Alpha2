using System;
using Common;
using Data;
using Infrastructure.Services;
using Services.PersistentProgress;
using Services.SaveLoad;
using UnityEngine;

namespace Infrastructure.GameStates
{
    public class LoadGameSettingsState : IState
    {
        private readonly GamesStateMachine _gamesStateMachine;
        private readonly ServiceLocator _serviceLocator;
        private IPersistentProgressService _persistentProgressService;
        public LoadGameSettingsState(GamesStateMachine gamesStateMachine, ServiceLocator serviceLocator)
        {
            _gamesStateMachine = gamesStateMachine;
            _serviceLocator = serviceLocator;
        }

        public void Enter() => 
            LoadSettings(NextState);
        
        private void NextState() => 
            _gamesStateMachine.Enter<InitializeInputState>();

        private void LoadSettings(Action callback)
        {
            ISaveLoadService saveLoadService = _serviceLocator.Single<ISaveLoadService>();
            _persistentProgressService = _serviceLocator.Single<IPersistentProgressService>();
            _persistentProgressService.Settings = saveLoadService.LoadSettings() ?? CreateSettings();
            callback?.Invoke();
        }

        private Settings CreateSettings()
        {
            Settings settings = new Settings();
            settings.SoundSettings.Mute = settings.StaticSoundSetting.Mute;
            settings.SoundSettings.Volume = settings.StaticSoundSetting.Volume;
                
            return settings;
        }

        public void Update(){}

        public void Exit(){}
    }
}