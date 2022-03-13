using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "New PickableObject Data", menuName = "StaticData/PickableObjectData")]
    public class PickableObjectStaticData : ScriptableObject
    {
        [Range(1, 30)] public int Value;
        public GameObject Prefab;
        public PickableObjectTypeId PickableObjectTypeId;
    }
}
