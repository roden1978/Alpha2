using Data;
using Infrastructure.Factories;
using PlayerScripts;
using Services.PersistentProgress;
using UnityEngine;

namespace Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IGameFactory _gameFactory;
        private const string Key = "Progress";

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
                Debug.Log(progressWriter.ToString());
            }
            PlayerPrefs.SetString(Key, _persistentProgressService.PlayerProgress.ToJSON());
        }

        public PlayerProgress LoadProgress() => 
            PlayerPrefs.GetString(Key)?
                .Deserialize<PlayerProgress>();
    }
}