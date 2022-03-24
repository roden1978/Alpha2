using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "New TrapStaticData", menuName = "StaticData/TrapData")]
    public class TrapStaticData : ScriptableObject
    {
        public TrapTypeId TrapTypeId;
        [Range(10, 20)] public int Damage;
        [Range(1f,2f)]public float Cooldown;
    }
}