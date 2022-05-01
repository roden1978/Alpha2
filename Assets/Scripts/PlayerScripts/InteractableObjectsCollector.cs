using System;
using System.Collections.Generic;
using EnemyScripts;
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
                { typeof(Crystal), CrystalCollect },
                { typeof(Fruit), FruitCollect },
                { typeof(Food), FoodCollect },
                { typeof(Bullet), DamageCollect },
                { typeof(Life), LifeCollect },
                { typeof(Trap), DamageCollect},
                { typeof(OctonoidAttack), DamageCollect}
            };
        }

        public void Collect(InteractableObject interactableObject)
        {
            foreach (var action in _actions)
                if(action.Key == interactableObject.GetType()) 
                {
                    action.Value.Invoke(interactableObject.Value);
                    if(interactableObject.Hide)
                        interactableObject.gameObject.SetActive(false);
                }
        }

        private void CrystalCollect(int crystals)
        {
            CrystalCollecting?.Invoke(crystals);
        }

        private void FruitCollect(int scores)
        {
            FruitCollecting?.Invoke(scores);
        }

        private void FoodCollect(int health)
        {
            FoodCollecting?.Invoke(health);
        }

        private void DamageCollect(int damage)
        {
            Debug.Log($"Damage {damage.ToString()}");
            DamageCollecting?.Invoke(damage);
        }

        private void LifeCollect(int life)
        {
            LifeCollecting?.Invoke(life);
        }
    }
}
