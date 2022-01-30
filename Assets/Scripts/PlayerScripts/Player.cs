using UnityEngine;

namespace PlayerScripts
{
    [RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D), typeof(Throw))]
    [RequireComponent(typeof(InteractableObjectsCollector))]
    public class Player : MonoBehaviour
    {
       private float _health;
      
        private void TakeDamage(float delta)
        {
            _health -= delta;
        }
    }
}
