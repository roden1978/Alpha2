using System;
using System.Collections.Generic;
using System.Linq;
using GameObjectsScripts;
using UnityEngine;

namespace Common
{
    public class BulletPools : MonoBehaviour
    {
        [SerializeField] private List<Pool> _pools;
        
        private Dictionary<Type, Queue<Bullet>> _poolsRepository;
        private int _index;
        private void Start()
        {
            _poolsRepository = new Dictionary<Type, Queue<Bullet>>(3);

            foreach (var pool in _pools)
            {
                _index = 0;
                var bulletPool = new Queue<Bullet>();
                for (var i = 0; i < pool.Capacity; i++)
                {
                    var bullet = Instantiate(pool.Bullet, Vector3.zero, Quaternion.identity, transform);
                    bullet.name = $"{bullet.GetType()}({i})";
                    bullet.gameObject.SetActive(false);
                    bulletPool.Enqueue(bullet);
                    _index = i;
                }
                _poolsRepository.Add(bulletPool.First().GetType(), bulletPool);
            }
        }

        public Bullet GetPooledObject(Type type)
        {
            var bullet = _poolsRepository[type].Peek();
            
            if (bullet.gameObject.activeInHierarchy)
            {
                var additional = Instantiate(bullet, Vector3.zero, Quaternion.identity, transform);
                additional.name = $"{additional.GetType()}({++_index})";
                _poolsRepository[type].Enqueue(additional);
                return additional;
            }

            bullet = _poolsRepository[type].Dequeue();
            bullet.gameObject.SetActive(true);
            _poolsRepository[type].Enqueue(bullet);
            
            return bullet;
        }
        
        [Serializable]
        private class Pool
        {
            [SerializeField] private Bullet _bullet;

            [SerializeField] private int _size;
            public int Capacity => _size;
            public Bullet Bullet => _bullet;
        }
    }
}
