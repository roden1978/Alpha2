using UnityEngine;

namespace Common
{
   public class Fader : MonoBehaviour
   {
      private const string FadeOutAnimation = "FadeOutAnimation";
      private const string FadeInAnimation = "FadeInAnimation";
      private Animator _animator;

      private void Start()
      {
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
