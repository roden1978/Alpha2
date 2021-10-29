using UnityEngine;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        private void Start()
        {
            Time.timeScale = 0;
        }

        public void OnStartButton()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
