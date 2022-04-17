using GameObjectsScripts;
using UnityEngine;

namespace PlayerScripts
{
    public class GroundingFx : MonoBehaviour, IShowable
    {
        [SerializeField] private ParticleSystem _groundingFx;
        public void Show()
        {
            _groundingFx.Play(true);
        }

        public void Hide()
        {
           
        }
    }
}
