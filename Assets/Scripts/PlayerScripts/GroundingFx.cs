using GameObjectsScripts;
using UnityEngine;

namespace PlayerScripts
{
    public class GroundingFx : MonoBehaviour, IShowable
    {
        [SerializeField] private ParticleSystem _groundingFx;
        [SerializeField] private AudioSource _groundingSoundFx;
        public void Show()
        {
            _groundingFx.Play(true);
            _groundingSoundFx.Play();
        }

        public void Hide()
        {
           
        }
    }
}
