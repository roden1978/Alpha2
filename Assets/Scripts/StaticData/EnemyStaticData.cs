using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "New EnemyData", menuName = "StaticData/EnemyData")]
    public class EnemyStaticData : ScriptableObject
    {
        public EnemyTypeId EnemyTypeId;
        [Range(10, 50)] public int Health;
        [Range(.1f, 2f)] public float Cooldown;
        [Range(5, 50)] public int DamageOnTouch;
        [Range(0, 10)] public int SelfDamageOnTouch;
        public GameObject Prefab;
    }
}