using System;
using System.Collections.Generic;
using System.Linq;
using GameObjectsScripts;
using UnityEngine;

namespace PlayerScripts
{
    public class InteractableObjectsCollector : MonoBehaviour
    {
        private Dictionary<Type, Action<int>> _actions;

        public event Action<int> OnCrystalCollect;
        public event Action<int> OnFruitCollect;
        public event Action<int> OnFoodCollect;
        public event Action<int> OnDamage;

        private void Awake()
        {
            _actions = new Dictionary<Type, Action<int>>
            {
                {typeof(Crystal), CrystalCollect},
                {typeof(Fruit), FruitCollect},
                {typeof(Food), FoodCollect},
                {typeof(Bullet), DamageCollect}
            };
        }

        private void OnTriggerEnter2D(Collider2D otherCollider)
        {
            if (otherCollider.gameObject.TryGetComponent(out InteractableObject interactableObject))
            {
                foreach (var action in _actions
                    .Where(action => action.Key == interactableObject.GetType()))
                {
                    action.Value.Invoke(interactableObject.Value);
                    interactableObject.gameObject.SetActive(false);
                }
            }
        }

        private void CrystalCollect(int crystals)
        {
            Debug.Log($"crystals {crystals}");
            OnCrystalCollect?.Invoke(crystals);
        }
        private void FruitCollect(int scores)
        {
            Debug.Log($"scores {scores}");
            OnFruitCollect?.Invoke(scores);
        }
        private void FoodCollect(int health)
        {
            Debug.Log($"health {health}");
            OnFoodCollect?.Invoke(health);
        }

        private void DamageCollect(int damage)
        {
            Debug.Log($"damage {damage}");
            OnDamage?.Invoke(damage);
        }

    }
}
