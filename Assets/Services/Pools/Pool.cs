using System;
using Common;
using UnityEngine;

namespace Services.Pools
{
   
        [Serializable]
        public class Pool: IPool
        {
            public Pool(PooledObject pooledObject, int capacity)
            {
                PooledObject = pooledObject;
                Capacity = capacity;
            }

            public int Capacity { get; }

            public PooledObject PooledObject { get; }
        }
}