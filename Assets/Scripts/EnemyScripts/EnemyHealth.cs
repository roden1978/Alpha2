using System;
using PlayerScripts;
using UnityEngine;

namespace EnemyScripts
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        public int HP { get; private set; }
        public int MaxHealth { get; private set; }

        public Action HealthUpdate;

        public void Construct(int health)
        {
            MaxHealth = HP = health;
        }
        public void TakeDamage(int damage)
        {
            HP -= damage;
            HealthUpdate?.Invoke();
        }

        public void TakeHealth(int health)
        {
            if (HP < MaxHealth)
            {
                HP += health;
                HealthUpdate?.Invoke();
            }
        }
    }
}