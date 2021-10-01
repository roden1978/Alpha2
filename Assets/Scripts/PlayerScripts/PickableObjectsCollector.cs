using System;
using System.Collections.Generic;
using System.Linq;
using GameObjectsScripts;
using UnityEngine;

namespace PlayerScripts
{
    public class PickableObjectsCollector : MonoBehaviour
    {
        private Dictionary<Type, Action<int>> _actions;

        public event Action<int> OnCrystalCollect;
        public event Action<int> OnFruitCollect;
        public event Action<int> OnFoodCollect;

        private void Awake()
        {
            _actions = new Dictionary<Type, Action<int>>
            {
                {typeof(Crystal), CrystalCollect},
                {typeof(Fruit), FruitCollect},
                {typeof(Food), FoodCollect}

            };
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.TryGetComponent(out PickableObject pickableObject)) return;
            
            foreach (var action in _actions
                .Where(action => action.Key == pickableObject.GetType()))
            {
                action.Value.Invoke(pickableObject.Value);
                pickableObject.gameObject.SetActive(false);
            }
        }

        private void CrystalCollect(int value)
        {
            Debug.Log($"value {value}");
            OnCrystalCollect?.Invoke(value);
        }
        private void FruitCollect(int value)
        {
            Debug.Log($"value {value}");
            OnFruitCollect?.Invoke(value);
        }
        private void FoodCollect(int value)
        {
            Debug.Log($"value {value}");
            OnFoodCollect?.Invoke(value);
        }

    }
}
