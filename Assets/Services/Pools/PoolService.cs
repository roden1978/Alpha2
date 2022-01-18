using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using UnityEngine;

namespace Services.Pools
{
    public class PoolService : MonoBehaviour, IPoolService
    {
        [SerializeField] private  List<Pool> _pools;
        private Dictionary<Type, Queue<PooledObject>> _poolsRepository;
        private int _index;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _poolsRepository = new Dictionary<Type, Queue<PooledObject>>(_pools.Capacity);
            foreach (Pool pool in _pools)
            {
                _index = 0;
                var pooledObjectsQueue = new Queue<PooledObject>();
                for (int i = 0; i < pool.Capacity; i++)
                {
                    PooledObject pooledObject = Instantiate(pool.PooledObject, Vector3.zero, Quaternion.identity, transform);
                    pooledObject.name = $"{pooledObject.GetType().Name}({i})";
                    pooledObject.gameObject.SetActive(false);
                    pooledObjectsQueue.Enqueue(pooledObject);
                    _index = i;
                }
                _poolsRepository.Add(pooledObjectsQueue.First().GetType(), pooledObjectsQueue);
            }
        }

        public PooledObject GetPooledObject(Type type)
        {
            PooledObject pooledObject = _poolsRepository[type].Peek();
            
            if (pooledObject.gameObject.activeInHierarchy)
            {
                PooledObject additional = Instantiate(pooledObject, Vector3.zero, Quaternion.identity, transform);
                additional.name = $"{additional.GetType().Name}({++_index})";
                _poolsRepository[type].Enqueue(additional);
                return additional;
            }

            pooledObject = _poolsRepository[type].Dequeue();
            pooledObject.gameObject.SetActive(true);
            _poolsRepository[type].Enqueue(pooledObject);
            
            return pooledObject;
        }
        
        [Serializable]
        public class Pool
        {
            public Pool(PooledObject pooledObject, int capacity)
            {
                PooledObject = pooledObject;
                Capacity = capacity;
            }

            public int Capacity;

            public PooledObject PooledObject;
        }
    }
}
