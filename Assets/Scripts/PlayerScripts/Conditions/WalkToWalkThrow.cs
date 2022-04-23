using Common;
using Services.Input;

namespace PlayerScripts.Conditions
{
    public class WalkToWalkThrow : ICondition
    {
        private bool _isShoot;

        public WalkToWalkThrow(IInputService inputService)
        {
            inputService.OnShoot += Shoot;
            inputService.OnStopShoot += StopShoot;
        }

        private void StopShoot()
        {
            _isShoot = false;
        }

        private void Shoot()
        {
            _isShoot = true;
        }

        public bool Result()
        {
            return _isShoot;
        }
    }
}