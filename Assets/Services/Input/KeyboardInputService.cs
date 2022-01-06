using System;

namespace Input
{
    public class KeyboardInputService : IDisposable, IInputService
    {
        private readonly PlayerInput _input;

        public KeyboardInputService()
        {
            _input = new PlayerInput();
            _input.Enable();
        }

       public float Jump()
        {
            return _input.Player.Jump.ReadValue<float>();
        }

        public float Shoot()
        {
            return _input.Player.Shoot.ReadValue<float>();
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
