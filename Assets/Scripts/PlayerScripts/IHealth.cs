namespace PlayerScripts
{
    public interface IHealth
    {
        public int HP { get; }
        public int MaxHealth { get; }
        void TakeDamage(int damage);
        void TakeHealth(int health);
    }
}