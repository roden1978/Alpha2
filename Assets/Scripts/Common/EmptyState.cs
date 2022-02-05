using System;

namespace Common
{
    public class EmptyState: IState
    {
        public void Enter()
        {
            throw new NotImplementedException();
        }

        public Type Update()
        {
            throw new NotImplementedException();
        }

        public Type FixedTick()
        {
            throw new NotImplementedException();
        }

        public void Exit()
        {
            throw new NotImplementedException();
        }
    }
}