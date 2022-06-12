using Data;
using Infrastructure.Factories;
using Services.PersistentProgress;
using UnityEngine;

namespace Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string Key = "Progress";
        private const string Settings = "Settings";
        
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IGameFactory _gameFactory;

        public SaveLoadService(IPersistentProgressService persistentProgressService, IGameFactory gameFactory)
        {
            _persistentProgressService = persistentProgressService;
            _gameFactory = gameFactory;
        }
        public void SaveProgress()
        {
            foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
            {
                progressWriter.UpdateProgress(_persistentProgressService.PlayerProgress);
            }
            PlayerPrefs.SetString(Key, _persistentProgressService.PlayerProgress.ToJSON());
        }

        public PlayerProgress LoadProgress() => 
            PlayerPrefs.GetString(Key)?
                .Deserialize<PlayerProgress>();

        public void SaveSettings() => 
            PlayerPrefs.SetString(Settings, _persistentProgressService.Settings.ToJSON());

        public Settings LoadSettings()=> 
                PlayerPrefs.GetString(Settings)?
                    .Deserialize<Settings>();
    }
}