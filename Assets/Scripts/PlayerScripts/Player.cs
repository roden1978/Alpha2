using UnityEngine;

namespace PlayerScripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        private float _health;

        private void ChangeHealth(float delta)
        {
            
        }

        private void TakeDamage(float delta)
        {
            _health -= delta;
        }
    }
}
