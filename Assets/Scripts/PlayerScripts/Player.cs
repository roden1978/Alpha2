using UnityEngine;

namespace PlayerScripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _maxVelocity;
        private float _health;
        public float Speed => _speed;
        public float MaxVelocity => _maxVelocity;

        private void ChangeHealth(float delta)
        {
            
        }

        private void TakeDamage(float delta)
        {
            _health -= delta;
        }
        
        
    }
}
