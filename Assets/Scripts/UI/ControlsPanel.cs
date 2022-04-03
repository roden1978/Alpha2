using System;
using System.Data;
using GameObjectsScripts;
using PlayerScripts;
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

namespace UI
{
    public class ControlsPanel : MonoBehaviour, IShowable
    {
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _shootButton;
        private Crowbar _crowbar;
        [field: SerializeField] public OnScreenStick OnScreenStick { get; private set; } 
        public Vector2 JoystickStartPosition { get; private set; }
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            _pauseButton.onClick.AddListener(Pause);
            _shootButton.onClick.AddListener(Shoot);
            JoystickStartPosition = ((RectTransform)OnScreenStick.transform).anchoredPosition;
        }

        public void Construct(Crowbar crowbar)
        {
            _crowbar = crowbar;
        }
        private void Shoot()
        {
            _crowbar.Shoot();
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
