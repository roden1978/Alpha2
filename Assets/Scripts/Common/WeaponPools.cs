using System;
using System.Collections.Generic;
using System.Linq;
using GameObjectsScripts;
using UnityEngine;

namespace Common
{
    public class WeaponPools : MonoBehaviour
    {
        [Serializable]
        public class Pool
        {
            [SerializeField] private Weapon _weapon;

            [SerializeField] private int _size;
            public int Capacity => _size;
            public Weapon Weapon => _weapon;
        }

        [SerializeField] private List<Pool> _pools;
        private Dictionary<Type, Queue<Weapon>> _poolsRepository;

        private void Start()
        {
            _poolsRepository = new Dictionary<Type, Queue<Weapon>>(3);

            foreach (var pool in _pools)
            {
                var weaponPool = new Queue<Weapon>();
                for (var i = 0; i < pool.Capacity; i++)
                {
                    var obj = Instantiate(pool.Weapon, Vector3.zero, Quaternion.identity);
                    obj.gameObject.SetActive(false);
                    weaponPool.Enqueue(obj);
                }
                
                _poolsRepository.Add(weaponPool.First().GetType(), weaponPool);
            }
        }
    }
}
