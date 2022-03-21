using System;
using UnityEngine;

namespace StaticData
{
    [Serializable]
    public class SaveProgressPointSpawnData
    {
        public string Id;
        public SaveProgressPointTypeId SaveProgressPointTypeId;
        public float Width;
        public float Height;
        public Vector3 Position;

        public SaveProgressPointSpawnData(string id, SaveProgressPointTypeId saveProgressPointTypeId, 
            float width, float height, Vector3 position)
        {
            Id = id;
            SaveProgressPointTypeId = saveProgressPointTypeId;
            Width = width;
            Height = height;
            Position = position;
        }
    }
}