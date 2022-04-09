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
            EventTrigger.Entry entryJump = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerDown
            };
            entryJump.callback.AddListener( ( _ ) => { Jump(); } );
            _jumpTrigger.triggers.Add(entryJump);

            EventTrigger.Entry entryShoot = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerDown
            };
            entryShoot.callback.AddListener( ( _ ) => { Shoot(); } );
            _shootTrigger.triggers.Add(entryShoot);
            
            JoystickStartPosition = ((RectTransform)OnScreenStick.transform).anchoredPosition;
        }

        private void Jump()
        {
            Debug.Log("Jump");
            _crowbar.Jump();
        }

        private void Shoot()
        {
            Debug.Log("Shoot");
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
