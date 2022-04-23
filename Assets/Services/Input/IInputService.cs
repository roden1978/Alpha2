using System;
using Infrastructure.Services;

namespace Services.Input
{
    public interface IInputService : IService
    {
        public event Action OnShoot;
        public event Action OnStopShoot;
        public event Action OnJump;
        void Jump();
        void Shoot();
        float Move();
    }
}