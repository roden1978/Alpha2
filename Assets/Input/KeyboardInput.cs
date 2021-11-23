using System;

namespace Input
{
    public class KeyboardInput : IDisposable, IPlayerInput
    {
        private readonly PlayerInput _input;

        public KeyboardInput()
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
