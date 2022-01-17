namespace Services.Pools
{
    public interface IPool
    {
        public int Capacity { get; }

        public PooledObject PooledObject { get; }
    }
}