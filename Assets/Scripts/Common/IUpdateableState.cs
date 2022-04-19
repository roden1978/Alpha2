using System;

namespace Common
{
    public interface IUpdateableState
    {
        public void Tick();
        public void Exit();
    }
}