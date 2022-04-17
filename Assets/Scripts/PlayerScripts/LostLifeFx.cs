using GameObjectsScripts;
using UnityEngine;

namespace PlayerScripts
{
    public class LostLifeFx : MonoBehaviour, IShowable
    {
        [SerializeField] private ParticleSystem _lostLifeFx;
        public void Show()
        {
            _lostLifeFx.Play(true);
        }

        public void Hide()
        {
            _lostLifeFx.Stop(true);
        }
    }
}
