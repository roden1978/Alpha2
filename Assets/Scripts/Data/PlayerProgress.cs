using System;

namespace Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;

        public PlayerProgress(string sceneName, int sceneIndex)
        {
            WorldData = new WorldData(sceneName, sceneIndex);
        }
    }
}