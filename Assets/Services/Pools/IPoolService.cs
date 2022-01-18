using System;

namespace Services.Pools
{
    public interface IPoolService
    {
        public PooledObject GetPooledObject(Type type);
    }
}