using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure
{
    public class Hud : MonoBehaviour
    {
        [field: SerializeField] public TMP_Text FruitsAmount { get; private set; }
        [field: SerializeField] public TMP_Text CrystalsAmount { get; private set; }
        [field: SerializeField] public LivesPanel LivesPanel { get; private set; }
        [field: SerializeField] public Image HealthBar { get; private set; }
    }
}