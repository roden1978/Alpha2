using UnityEngine;

namespace PlayerScripts
{
    public class ShootPoint : MonoBehaviour
    {
        [SerializeField] private Throw _throw;
        
        public void Throw()
        {
            _throw.ThrowWeapon(transform);
        }
    }
}
