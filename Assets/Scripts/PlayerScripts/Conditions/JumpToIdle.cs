using Common;
using Services.Input;

namespace PlayerScripts.Conditions
{
    public class JumpToIdle : ICondition
    {
        private readonly IDipstick _dipstick;

        public JumpToIdle(IDipstick dipstick) => 
            _dipstick = dipstick;

        public bool Result() => 
            _dipstick.Contact();
    }
}