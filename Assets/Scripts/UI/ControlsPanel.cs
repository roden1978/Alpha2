using System;
using PlayerScripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

namespace UI
{
    public class ControlsPanel : MonoBehaviour
    {
        [field: SerializeField] public OnScreenStick OnScreenStick { get; private set; }
        [SerializeField] private Button _pauseButton;
        [SerializeField] private EventTrigger _jumpTrigger;
        [SerializeField] private EventTrigger _shootTrigger;
        public Vector2 JoystickStartPosition { get; private set; }
        public GameMenu GameMenu { get; private set; }
        private Crowbar _crowbar;

        public void Construct(Crowbar crowbar) => 
            _crowbar = crowbar;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            RegisterPauseButton();
            RegisterJumpTrigger();
            RegisterShootTrigger();
            RegisterStopShootTrigger();
            RegisterJoystickPosition();
        }

        private void RegisterPauseButton()
        {
            _pauseButton.onClick.AddListener(Pause);
        }

        private void RegisterJoystickPosition()
        {
            JoystickStartPosition = ((RectTransform)OnScreenStick.transform).anchoredPosition;
        }

        private void RegisterStopShootTrigger()
        {
            EventTrigger.Entry entryStopShoot = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerUp
            };
            entryStopShoot.callback.AddListener((_) => { StopShoot(); });
            _shootTrigger.triggers.Add(entryStopShoot);
        }

        private void RegisterShootTrigger()
        {
            EventTrigger.Entry entryShoot = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerDown
            };
            entryShoot.callback.AddListener((_) => { Shoot(); });
            _shootTrigger.triggers.Add(entryShoot);
        }

        private void RegisterJumpTrigger()
        {
            EventTrigger.Entry entryJump = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerDown
            };
            entryJump.callback.AddListener((_) => { Jump(); });
            _jumpTrigger.triggers.Add(entryJump);
        }

        private void StopShoot() => 
            _crowbar.StopShoot();

        private void Jump() => 
            _crowbar.Jump();

        private void Shoot() => 
            _crowbar.Shoot();

        public void SetGameMenu(GameMenu gameMenu) => 
            GameMenu = gameMenu;

        private void Pause()
        {
            GameMenu.Pause.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        private void OnEnable()
        {
            if (SystemInfo.deviceType == DeviceType.Desktop)
                gameObject.SetActive(false);
        }
    }
}
