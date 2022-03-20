using StaticData;
using UnityEngine;

namespace Infrastructure.SavePointSpawners
{
    public class SaveProgressPointMarker : MonoBehaviour
    {
        [field: SerializeField]
        public SaveProgressPointStaticData SaveProgressPointStaticData { get; private set; }
        
    }
}