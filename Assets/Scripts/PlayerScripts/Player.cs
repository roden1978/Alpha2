using System;
using Infrastructure;
using UnityEngine;

namespace PlayerScripts
{
    [RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D), typeof(Throw))]
    [RequireComponent(typeof(InteractableObjectsCollector))]
    public class Player : MonoBehaviour//ISaveProgress
    {
       private float _health;
       public Action Transition;
       public Action Death;

       private void Start()
       {
           _health = Game.GamePlayerData.CurrentHealth;
       }

       public void TakeDamage(float delta)
        {
            _health -= delta;
            if(_health < 0)
            {
                Game.GamePlayerData.CurrentLivesAmount -= 1;
                if(Game.GamePlayerData.CurrentLivesAmount < 0) Debug.Log("GameOver");

                Debug.Log("Death");
                Death?.Invoke();
            }
        } 
        public void TakeHealth(float delta)
        {
            if(_health < Game.GamePlayerData.MaxHealth)
            {
                _health += delta;
            }
        }

        public void TransitionToNextScene()
        {
            SaveProgress();
            Transition?.Invoke();
        }

        private void SaveProgress()
        {
            Game.PlayerData.Health = Game.GamePlayerData.CurrentHealth;
            Game.PlayerData.CrystalsAmount = Game.GamePlayerData.CurrentCrystalsAmount;
            Game.PlayerData.FruitScoresAmount = Game.GamePlayerData.CurrentFruitScoresAmount;
            Game.GamePlayerData.CurrentLivesAmount = Game.GamePlayerData.CurrentLivesAmount;
            Game.PlayerData.SceneIndex = Game.GamePlayerData.CurrentScene;
        }
        
    }
}
