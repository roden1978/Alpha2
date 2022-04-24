using System;

namespace Services.Input
{
    public class KeyboardInputService : IDisposable, IInputService
    {
        private readonly PlayerInput _input;
        public event Action OnShoot;
        public event Action OnStopShoot;
        public event Action OnJump;
        public KeyboardInputService()
        {
            _input = new PlayerInput();
            _input.Enable();
            _input.Player.Shoot.started += _ => Shoot();
            _input.Player.Shoot.canceled += _ => StopShoot();
            _input.Player.Jump.performed += _ => Jump();
        }

       public void Jump()
        {
            OnJump?.Invoke();
        }

        public void Shoot()
        {
            OnShoot?.Invoke();
        }
        
        public void StopShoot()
        {
            OnStopShoot?.Invoke();
        }

        public float Move()
        {
            return _input.Player.Move.ReadValue<float>();
        }

        public void Dispose()
        {
            _input.Disable();
            _input?.Dispose();
        }
    }
}
