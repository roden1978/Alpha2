using System;

namespace Data
{
    [Serializable]
    public class WorldData
    {
        public PositionOnLevel PositionOnLevel;

        public WorldData(string sceneName)
        {
            PositionOnLevel = new PositionOnLevel(new Vector3Data(), sceneName);
        }
    }

    [Serializable]
    public class PositionOnLevel
    {
        public Vector3Data Position;
        public string SceneName;
        public int SceneIndex;

        public PositionOnLevel(Vector3Data position, string sceneName)
        {
            Position = position;
            SceneName = sceneName;
        }
    }
}