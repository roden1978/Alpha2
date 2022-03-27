using System;
using UnityEngine;

namespace Common
{
    public class Fader : MonoBehaviour
    {
        private const string FadeOutAnimation = "FadeOutAnimation";
        private const string FadeInAnimation = "FadeInAnimation";
        [SerializeField] private Animator _animator;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void FadeIn()
        {
            gameObject.SetActive(true);
            _animator.Play(FadeInAnimation);
        }

        public void FadeOut()
        {
            _animator.Play(FadeOutAnimation);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}