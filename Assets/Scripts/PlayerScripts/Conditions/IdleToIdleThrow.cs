namespace PlayerScripts.Conditions
{
    public class IdleToIdleThrow : ICondition
    {
        private readonly bool _isShoot;

        public IdleToIdleThrow(bool isShoot) => 
            _isShoot = isShoot;

        public bool Examination() => 
            _isShoot;
    }
}