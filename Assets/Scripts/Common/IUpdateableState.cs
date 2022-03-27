using System;

namespace Common
{
    public interface IUpdateableState
    {
        public Type Update();
        public void Exit();
    }
}