using System;

namespace Services.Input
{
    public interface IInputService
    {
        public event Action OnShoot;
        public event Action OnJump;
        void Jump();
        void Shoot();
        float Move();
    }
}