using System;

namespace Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;
        public PlayerState PlayerState;
        public StaticPlayerData StaticPlayerData;
        public KillData KillData;
        public PickableObjectData PickableObjectData;
        public SaveProgressPointData SaveProgressPointData;

        public PlayerProgress(string sceneName)
        {
            WorldData = new WorldData(sceneName);
            PlayerState = new PlayerState();
            StaticPlayerData = new StaticPlayerData();
            KillData = new KillData();
            PickableObjectData = new PickableObjectData();
            SaveProgressPointData = new SaveProgressPointData();
        }

        
    }
}