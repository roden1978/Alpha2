using System;
using UnityEngine;

namespace PlayerScripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    [RequireComponent(typeof(Throw))]
    [RequireComponent(typeof(InteractableObjectsCollector))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _maxVelocity;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _xMoveDamping = 0.3f;
        [SerializeField] private float _yMoveDamping = 0.3f;

        private Dipstick _dipstick;

        public event Action OnShoot;

        private void Awake()
        {
            _dipstick = new Dipstick(this);
        }

        private float _health;
        public float Speed => _speed;
        public float MaxVelocity => _maxVelocity;
        public float JumpForce => _jumpForce;
        public float XMoveDamping => _xMoveDamping;
        public float YMoveDamping => _yMoveDamping;

        private void ChangeHealth(float delta)
        {
            
        }

        private void TakeDamage(float delta)
        {
            _health -= delta;
        }

        public bool StayOnGround()
        {
            return _dipstick.Contact();
        }

        public void InvokeShootAction()
        {
            OnShoot?.Invoke();
        }

    }
}
