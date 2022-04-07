using System;
using PlayerScripts;
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

namespace UI
{
    public class ControlsPanel : MonoBehaviour
    {
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _shootButton;
        [SerializeField] private Button _jumpButton;
        [field: SerializeField] public OnScreenStick OnScreenStick { get; private set; }
        public Vector2 JoystickStartPosition { get; private set; }
        public GameMenu GameMenu { get; private set; }
        private Crowbar _crowbar;

        public void Construct(Crowbar crowbar)
        {
            _crowbar = crowbar;
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            _pauseButton.onClick.AddListener(Pause);
            _shootButton.onClick.AddListener(Shoot);
            _jumpButton.onClick.AddListener(Jump);
            JoystickStartPosition = ((RectTransform)OnScreenStick.transform).anchoredPosition;
        }

        private void Jump()
        {
            _crowbar.Jump();
        }

        private void Shoot()
        {
            _crowbar.Shoot();
        }
        public void SetGameMenu(GameMenu gameMenu)
        {
            GameMenu = gameMenu;
        }

        private void Pause()
        {
            GameMenu.Pause.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
