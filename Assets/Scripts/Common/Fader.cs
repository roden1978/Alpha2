using System;
using UnityEngine;

namespace Common
{
   public class Fader : MonoBehaviour
   {
      private const string FadeOutAnimation = "FadeOutAnimation";
      private const string FadeInAnimation = "FadeInAnimation";
      [SerializeField] private CanvasGroup _curtain;
      private Animator _animator;

      private void Awake()
      {
         DontDestroyOnLoad(this);
      }

      private void Start()
      {
         gameObject.SetActive(true);
         
         _animator = GetComponent<Animator>();
      }

      public void FadeIn()
      {
         _animator.Play(FadeInAnimation);
      }

      public void FadeOut()
      {
         _animator.Play(FadeOutAnimation);
      }
   }
}
