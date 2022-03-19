using System;
using Data;
using Services.PersistentProgress;
using UnityEngine;

namespace PlayerScripts
{
    [RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D), typeof(Throw))]
    [RequireComponent(typeof(InteractableObjectsCollector))]
    public class Player : MonoBehaviour, ISavedProgress, IHealth
    {
        private int _currentLivesAmount;
        public Action<int> Death;

        public int HP { get; private set; }
        public int MaxHealth { get; private set; }
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
            if(HP < MaxHealth)
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
            MaxHealth = playerProgress.PlayerState.MaxHealth;
        }

        public void UpdateProgress(PlayerProgress playerProgress)
        {
            playerProgress.PlayerState.CurrentHealth = HP;
            playerProgress.PlayerState.CurrentLivesAmount = _currentLivesAmount;
        }
    }
}
