using StaticData;
using UnityEngine;

namespace Infrastructure.EnemySpawners
{
    public class EnemySpawnMarker : MonoBehaviour
    {
        [field: SerializeField]
        public EnemyStaticData EnemyStaticData { get; private set; }
    }
}