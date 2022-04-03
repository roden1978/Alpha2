using UnityEngine;

namespace PlayerScripts
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Throw _throw;
        public void ThrowWeapon()
        {
            _throw.ThrowWeapon();
        }
    }
}
