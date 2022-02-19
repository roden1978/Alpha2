using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private TMP_Text _fruitsAmount;
        [SerializeField] private TMP_Text _crystalsAmount;
        [SerializeField] private LivesPanel _livesPanel;
        [SerializeField] private Slider _healthBar;
        public TMP_Text FruitsAmount => _fruitsAmount;
        public TMP_Text CrystalsAmount => _crystalsAmount;
        public LivesPanel LivesPanel => _livesPanel;
        public Slider HealthBar=> _healthBar;
    }
}