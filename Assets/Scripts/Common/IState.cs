using System;

namespace Common
{
    public interface IState : IUpdateableState
    {
        public void Enter();
    }

    public interface IUpdateableState
    {
        public Type Update();
        public void Exit();
    }
    
    public interface IPayloadState<in TPayload> : IUpdateableState
    {
        public void Enter(TPayload payload);
    }
    
}