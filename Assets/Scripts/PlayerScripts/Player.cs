using System;
using Data;
using UnityEngine;

namespace PlayerScripts
{
    public interface IHealth
    {
        public int HP { get; }
        void TakeDamage(int damage);
        void TakeHealth(int health);
    }

    [RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D), typeof(Throw))]
    [RequireComponent(typeof(InteractableObjectsCollector))]
    public class Player : MonoBehaviour, ISavedProgress, IHealth
    {
        private int _currentLivesAmount;
        public Action<int> Death;
        private int _maxHealth;

        public int HP { get; private set; }

        public void TakeDamage(int damage)
        {
            HP -= damage;
            if(HP < 0)
            {
                Death?.Invoke(_currentLivesAmount -= 1);
            }
        } 
        public void TakeHealth(int health)
        {
            if(HP < _maxHealth)
            {
                HP += health;
            }
        }

        public void TakeBonusLive(int delta)
        {
            _currentLivesAmount += delta;
        }

        public void LoadProgress(PlayerProgress playerProgress)
        {
            HP = playerProgress.PlayerState.CurrentHealth;
            _currentLivesAmount = playerProgress.PlayerState.CurrentLivesAmount;
            _maxHealth = playerProgress.PlayerState.MaxHealth;
        }

        public void UpdateProgress(PlayerProgress playerProgress)
        {
            playerProgress.PlayerState.CurrentHealth = HP;
            playerProgress.PlayerState.CurrentLivesAmount = _currentLivesAmount;
        }
    }
}
