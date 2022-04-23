namespace PlayerScripts.Conditions
{
    public class JumpToJumpProxy : ICondition
    {
        private readonly IDipstick _dipstick;

        public JumpToJumpProxy(IDipstick dipstick)
        {
            _dipstick = dipstick;
        }

        public bool Examination()
        {
            return _dipstick.Contact();
        }
    }
}