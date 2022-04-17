using GameObjectsScripts;
using UnityEngine;

namespace PlayerScripts
{
    public class JumpFx : MonoBehaviour, IShowable
    {
        [SerializeField] private ParticleSystem _jumpFx;
        public void Show()
        {
            _jumpFx.Play(true);
        }

        public void Hide()
        {
           _jumpFx.Stop(true);
        }
    }
}
