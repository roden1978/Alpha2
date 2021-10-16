using UnityEngine;

namespace PlayerScripts
{
    public class PlayerView : MonoBehaviour
    {
        private Throw _throw;
        private ShootPoint _shootPoint;
        private void Start()
        {
            _throw = gameObject.GetComponentInParent<Throw>();
            _shootPoint = _throw.GetComponentInChildren<ShootPoint>();
        }

        private void TrowWeapon()
        {
            var shootPointTransform = _shootPoint.transform; 
            _throw.ThrowAxe(shootPointTransform);
        }
    }
}
