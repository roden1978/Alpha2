using System;
using Services.Pools;
using UnityEngine;

namespace Common
{
    public interface IPoolService
    {
        public void Initialize();
        public PooledObject GetPooledObject(Type type);
    }
}