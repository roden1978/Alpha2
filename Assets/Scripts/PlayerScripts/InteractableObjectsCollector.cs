using System;
using System.Collections.Generic;
using GameObjectsScripts;
using UnityEngine;

namespace PlayerScripts
{
    public class InteractableObjectsCollector : MonoBehaviour
    {
        private Dictionary<Type, Action<int>> _actions;
        
        public event Action<int> CrystalCollecting;
        public event Action<int> FruitCollecting;
        public event Action<int> FoodCollecting;
        public event Action<int> DamageCollecting;
        public event Action<int> LifeCollecting;

        private void Start()
        {
            _actions = new Dictionary<Type, Action<int>>
            {
                {typeof(Crystal), CrystalCollect},
                {typeof(Fruit), FruitCollect},
                {typeof(Food), FoodCollect},
                {typeof(Bullet), DamageCollect},
                {typeof(Life), LifeCollect}
            };
        }

        public void Collect(InteractableObject interactableObject)
        {
            foreach (var action in _actions)
                if(action.Key == interactableObject.GetType()) 
                {
                    action.Value.Invoke(interactableObject.Value);
                    interactableObject.gameObject.SetActive(false);
                }
        }

        private void CrystalCollect(int crystals)
        {
            Debug.Log($"crystals {crystals}");
            CrystalCollecting?.Invoke(crystals);
        }

        private void FruitCollect(int scores)
        {
            FruitCollecting?.Invoke(scores);
        }

        private void FoodCollect(int health)
        {
            Debug.Log($"health {health}");
            FoodCollecting?.Invoke(health);
        }

        private void DamageCollect(int damage)
        {
            Debug.Log($"damage {damage}");
            DamageCollecting?.Invoke(damage);
        }

        private void LifeCollect(int life)
        {
            Debug.Log($"life {life}");
            LifeCollecting?.Invoke(life);
        }
    }
}
