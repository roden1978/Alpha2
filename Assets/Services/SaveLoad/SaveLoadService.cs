using Data;
using UnityEngine;

namespace Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string Key = "Progress";
        public void SaveProgress()
        {
            
        }

        public PlayerProgress LoadProgress() => 
            PlayerPrefs.GetString(Key)?
                .Deserialize<PlayerProgress>();
    }
}