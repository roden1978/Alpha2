using UnityEngine;

namespace PlayerScripts
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private ShootPoint _shootPoint;

        public void ThrowWeapon()
        {
            _shootPoint.Throw();
        }
    }
}
