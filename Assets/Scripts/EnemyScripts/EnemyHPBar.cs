using System;
using UnityEngine;
using UnityEngine.UI;

namespace EnemyScripts
{
    public class EnemyHPBar : MonoBehaviour
    {
        [SerializeField] private Image _image;

        public void SetValue(int current, int max)
        {
            _image.fillAmount = Convert.ToSingle(current / max);
            SetColour(current, max);
        }

        private void SetColour(int current, int max)
        {
            _image.color = current < max / 3 * 2 && current > max / 3 ? Color.yellow :
                current < max / 3 ? Color.red : Color.green;
        }
    }
}