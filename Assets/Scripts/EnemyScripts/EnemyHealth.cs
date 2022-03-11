using System;
using PlayerScripts;
using UnityEngine;

namespace EnemyScripts
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private int _health;
        [SerializeField] private int _maxHealth;
        public int HP => _health;
        public int MaxHealth => _maxHealth;
        public Action HealthUpdate;

        public void TakeDamage(int damage)
        {
            _health -= damage;
            HealthUpdate?.Invoke();
        }

        public void TakeHealth(int health)
        {
            if (_health < _maxHealth)
                _health += health;
        }
    }
}