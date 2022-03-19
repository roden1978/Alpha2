using System;
using UnityEngine;

namespace StaticData
{
    [Serializable]
    public class PickableObjectSpawnData
    {
        public string Id;
        public PickableObjectTypeId PickableObjectTypeId;
        public Vector3 Position;

        public PickableObjectSpawnData(string id, PickableObjectTypeId pickableObjectTypeId, Vector3 position)
        {
            Id = id;
            PickableObjectTypeId = pickableObjectTypeId;
            Position = position;
        }
    }
}