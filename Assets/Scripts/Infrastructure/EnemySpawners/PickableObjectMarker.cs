using StaticData;
using UnityEngine;

namespace Infrastructure.EnemySpawners
{
    public class PickableObjectMarker : MonoBehaviour
    {
        [field: SerializeField]
        public PickableObjectStaticData PickableObjectStaticData { get; private set; }
    }
}