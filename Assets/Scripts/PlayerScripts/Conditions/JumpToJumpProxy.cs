using Common;
using Services.Input;

namespace PlayerScripts.Conditions
{
    public class JumpToJumpProxy : ICondition
    {
        private readonly IDipstick _dipstick;
        private bool _isShoot;

        public JumpToJumpProxy(IDipstick dipstick, IInputService inputService)
        {
            _dipstick = dipstick;
            inputService.OnShoot += () => _isShoot = true;
            inputService.OnStopShoot += () => _isShoot = false;
        }

        public bool Result()
        {
            return _dipstick.Contact() || _isShoot;
        }
    }
}