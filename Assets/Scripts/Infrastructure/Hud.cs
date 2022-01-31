using TMPro;
using UnityEngine;

namespace Infrastructure
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private TMP_Text _fruitsAmount;

        public TMP_Text FruitsAmount => _fruitsAmount;
    }
}