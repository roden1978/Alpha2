using Common;
using Services.Input;
using UnityEngine;

namespace PlayerScripts.Conditions
{
    public class JumpToJumpThrow : ICondition
    {
        private bool _isShoot;
        private readonly IDipstick _dipstick;

        public JumpToJumpThrow(IDipstick dipstick, IInputService inputService)
        {
            _dipstick = dipstick;
            inputService.OnShoot += () => _isShoot = true;
            inputService.OnStopShoot += () => _isShoot = false;
        }

        public bool Result() => 
            !_dipstick.Contact() && _isShoot;
    }
}