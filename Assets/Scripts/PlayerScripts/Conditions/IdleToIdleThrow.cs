﻿using Services.Input;

namespace PlayerScripts.Conditions
{
    public class IdleToIdleThrow : ICondition
    {
        private bool _isShoot;
        public IdleToIdleThrow(IInputService inputService)
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