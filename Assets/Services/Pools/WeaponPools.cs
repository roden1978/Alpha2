using System;
using System.Collections.Generic;
using System.Linq;
using GameObjectsScripts;
using UnityEngine;

namespace Services.Pools
{
    public class WeaponPools : MonoBehaviour
    {
        [SerializeField] private List<Pool> _pools;

        private Dictionary<Type, Queue<Weapon>> _poolsRepository;
        private int _index;
        private void Start()
        {
            _poolsRepository = new Dictionary<Type, Queue<Weapon>>(3);

            foreach (var pool in _pools)
            {
                _index = 0;
                var weaponPool = new Queue<Weapon>();
                for (var i = 0; i < pool.Capacity; i++)
                {
                    var weapon = Instantiate(pool.Weapon, Vector3.zero, Quaternion.identity, transform);
                    weapon.name = $"{weapon.GetType()}({i})";
                    weapon.gameObject.SetActive(false);
                    weaponPool.Enqueue(weapon);
                    _index = i;
                }
                _poolsRepository.Add(weaponPool.First().GetType(), weaponPool);
            }
        }

        public Weapon GetPooledObject(Type type)
        {
            var weapon = _poolsRepository[type].Peek();
            
            if (weapon.gameObject.activeInHierarchy)
            {
                var additional = Instantiate(weapon, Vector3.zero, Quaternion.identity, transform);
                additional.name = $"{additional.GetType()}({++_index})";
                _poolsRepository[type].Enqueue(additional);
                return additional;
            }

            weapon = _poolsRepository[type].Dequeue();
            weapon.gameObject.SetActive(true);
            _poolsRepository[type].Enqueue(weapon);
            
            return weapon;
        }
        
        [Serializable]
        private class Pool
        {
            [SerializeField] private Weapon _weapon;

            [SerializeField] private int _size;
            public int Capacity => _size;
            public Weapon Weapon => _weapon;
        }
    }
}
