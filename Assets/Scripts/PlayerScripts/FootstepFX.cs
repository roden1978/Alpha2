using GameObjectsScripts;
using UnityEngine;

namespace PlayerScripts
{
    public class FootstepFX : MonoBehaviour, IShowable
    {
        [SerializeField] private ParticleSystem _footstepFx;
        public void Show()
        {
            _footstepFx.Play();
        }

        public void Hide()
        {
            _footstepFx.Stop();
        }
    }
}
