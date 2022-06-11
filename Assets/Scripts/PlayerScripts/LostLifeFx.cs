using GameObjectsScripts;
using UnityEngine;

namespace PlayerScripts
{
    public class LostLifeFx : MonoBehaviour, IShowable
    {
        [SerializeField] private ParticleSystem _lostLifeFx;
        [SerializeField] private ParticleSystem _angelFx;
        public void Show()
        {
            _lostLifeFx.Play(true);
            _angelFx.Play(true);
        }

        public void Hide()
        {
            _lostLifeFx.Stop(true);
            _angelFx.Stop(true);
        }
    }
}
