using System;

namespace Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;
        public PlayerState PlayerState;
        public StaticPlayerData StaticPlayerData; 
        public PlayerProgress(string sceneName, int sceneIndex)
        {
            WorldData = new WorldData(sceneName, sceneIndex);
            PlayerState = new PlayerState();
            StaticPlayerData = new StaticPlayerData();
        }
    }
}