using Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _start;
        [SerializeField] private Button _options;
        [SerializeField] private Button _about;
        [SerializeField] private Button _exit;
        [SerializeField] private About _aboutPanel;
        [SerializeField] private Settings _settingsPanel;
        private void Start()
        {
            _start.onClick.AddListener(OnStartButton);
            _options.onClick.AddListener(OnOptionsButton);
            _about.onClick.AddListener(OnAboutButton);
            _exit.onClick.AddListener(OnExitButton);
        }

        private void OnExitButton()
        {
            Application.Quit();
        }

        private void OnAboutButton()
        {
            HideMenu();
            _aboutPanel.gameObject.SetActive(true);
        }

        private void OnOptionsButton()
        {
            HideMenu();
            _settingsPanel.gameObject.SetActive(true);
        }

        private void OnStartButton()
        {
            HideMenu();
            Bootstrapper bootstrapper = FindObjectOfType<Bootstrapper>();
            bootstrapper.LoadLevelState();
        }

        private void HideMenu()
        {
            gameObject.SetActive(false);
        }
    }
}
