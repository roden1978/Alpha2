using Services.Input;

namespace PlayerScripts.Conditions
{
    public class WalkToWalkThrow : ICondition
    {
        private bool _isShoot;

        public WalkToWalkThrow(IInputService inputService)
        {
            inputService.OnShoot += Shoot;
        }

        private void Shoot()
        {
            _isShoot = true;
        }

        public bool Examination()
        {
            return _isShoot;
        }
    }
}