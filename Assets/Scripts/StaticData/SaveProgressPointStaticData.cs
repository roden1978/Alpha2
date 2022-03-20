using Infrastructure;
using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "New SaveProgressPointData", menuName = "StaticData/SaveProgressPointData")]
    public class SaveProgressPointStaticData : ScriptableObject
    {
        public SaveProgressPointTypeId SaveProgressPointTypeId;
        public float ColliderWidth;
        public float ColliderHeight;
        public SaveProgressPoint Prefab;
    }
}