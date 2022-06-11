using GameObjectsScripts;
using UnityEngine;

namespace PlayerScripts
{
    public class JumpSoundFx : MonoBehaviour, IShowable
    {
        [SerializeField] private AudioSource _jumpSoundFx;
        public void Show() => 
            _jumpSoundFx.Play();

        public void Hide() { }
    }
}
