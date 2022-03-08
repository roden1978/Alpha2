using System;
using Data;
using UnityEngine;

namespace PlayerScripts
{
    [RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D), typeof(Throw))]
    [RequireComponent(typeof(InteractableObjectsCollector))]
    public class Player : MonoBehaviour, ISavedProgress
    {
       private int _health;
       private int _currentLivesAmount;
       private int _maxHealth;
       public Action<int> Death;

      
       public void TakeDamage(int delta)
        {
            _health -= delta;
            if(_health < 0)
            {
                Death?.Invoke(_currentLivesAmount -= 1);
            }
        } 
        public void TakeHealth(int delta)
        {
            if(_health < _maxHealth)
            {
                _health += delta;
            }
        }

        public void TakeBonusLive(int delta)
        {
            _currentLivesAmount += delta;
        }

        public void LoadProgress(PlayerProgress playerProgress)
        {
            _health = playerProgress.PlayerState.CurrentHealth;
            _currentLivesAmount = playerProgress.PlayerState.CurrentLivesAmount;
            _maxHealth = playerProgress.PlayerState.MaxHealth;
        }

        public void UpdateProgress(PlayerProgress playerProgress)
        {
            playerProgress.PlayerState.CurrentHealth = _health;
            playerProgress.PlayerState.CurrentLivesAmount = _currentLivesAmount;
        }
    }
}
