using System.Collections;
using UnityEngine;

namespace Common
{
    public class Fader : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _curtain;
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public IEnumerator FadeIn()
        {
            while (_curtain.alpha > 0)
            {
                _curtain.alpha -= 0.01f;
                yield return null;
            }
            gameObject.SetActive(false);
        }

        public void Hide() => 
            StartCoroutine(FadeIn());

        public void Show()
        {
            _curtain.alpha = 1.0f;
            gameObject.SetActive(true);
        }

    }
}