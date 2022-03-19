using StaticData;
using UnityEngine;

namespace Infrastructure.EnemySpawners
{
    public class SpawnMarker : MonoBehaviour
    {
        [field: SerializeField]
        public EnemyStaticData EnemyStaticData { get; private set; }
    }
}