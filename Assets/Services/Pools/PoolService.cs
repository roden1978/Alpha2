using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using UnityEngine;

namespace Services.Pools
{
    public class PoolService : IPoolService
    {
        private readonly List<IPool> _pools;
        private readonly Dictionary<Type, Queue<PooledObject>> _poolsRepository;
        private int _index;

        public PoolService(List<IPool> pools)
        {
            _pools = pools;
            _poolsRepository = new Dictionary<Type, Queue<PooledObject>>(_pools.Capacity);
        }

        public void Initialize()
        {
            foreach (IPool pool in _pools)
            {
                _index = 0;
                var pooledObjectsQueue = new Queue<PooledObject>();
                for (int i = 0; i < pool.Capacity; i++)
                {
                    PooledObject pooledObject = UnityEngine.Object.Instantiate(pool.PooledObject, Vector3.zero, Quaternion.identity);
                    pooledObject.name = $"{pooledObject.GetType()}({i})";
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
                PooledObject additional = UnityEngine.Object.Instantiate(pooledObject, Vector3.zero, Quaternion.identity);
                additional.name = $"{additional.GetType()}({++_index})";
                _poolsRepository[type].Enqueue(additional);
                return additional;
            }

            pooledObject = _poolsRepository[type].Dequeue();
            pooledObject.gameObject.SetActive(true);
            _poolsRepository[type].Enqueue(pooledObject);
            
            return pooledObject;
        }
    }
}
