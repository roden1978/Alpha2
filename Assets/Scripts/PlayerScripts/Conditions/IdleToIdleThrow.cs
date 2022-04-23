using Common;
using Services.Input;
using UnityEngine;

namespace PlayerScripts.Conditions
{
    public class IdleToIdleThrow : ICondition
    {
        private bool _isShoot;
        public IdleToIdleThrow(IInputService inputService)
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
            //Debug.Log($"IdleToIdleThrow {_isShoot}");
            
            return _isShoot;
        }
    }
}