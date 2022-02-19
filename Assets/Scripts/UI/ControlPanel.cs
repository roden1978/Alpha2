using System;
using GameObjectsScripts;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ControlPanel : MonoBehaviour, IShowable
    {
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _shootButton;

        private void Start()
        {
            _pauseButton.onClick.AddListener(Pause);
            _shootButton.onClick.AddListener(Shoot);
        }

        private void Shoot()
        {
            throw new NotImplementedException();
        }

        private void Pause()
        {
            throw new NotImplementedException();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
