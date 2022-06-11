using GameObjectsScripts;
using UnityEngine;

namespace PlayerScripts
{
    public class FootstepFX : MonoBehaviour, IShowable
    {
        [SerializeField] private ParticleSystem _footstepFx;
        [SerializeField] private AudioSource _footstepSoundFx;
        public void Show()
        {
            _footstepFx.Play();
            _footstepSoundFx.Play();
        }

        public void Hide()
        {
            _footstepFx.Stop();
            _footstepSoundFx.Stop();
        }
    }
}
