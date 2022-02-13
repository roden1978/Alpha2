using Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Bootstrapper _bootstrapper;
        private void Start()
        {
            _startButton.onClick.AddListener(OnStartButton);
        }

        private void OnStartButton()
        {
            gameObject.SetActive(false);
            _bootstrapper.StartGame();
        }
    }
}
