using System;
using UnityEngine;

namespace StaticData
{
    [Serializable]
    public class PortalSpawnData
    {
        public string ToSceneName;
        public Vector3 Position;

        public PortalSpawnData(string toSceneName, Vector3 position)
        {
            ToSceneName = toSceneName;
            Position = position;
        }
    }
}