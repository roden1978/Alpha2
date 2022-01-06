using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace PlayerScripts
{
    [RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D), typeof(Throw))]
    [RequireComponent(typeof(InteractableObjectsCollector))]
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

        

        public void InvokeShootAction()
        {
            
        }

    }
}
