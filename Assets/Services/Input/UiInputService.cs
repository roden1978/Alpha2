using System;
using UnityEngine;

namespace Input
{
    public class UiInputService : IDisposable, IInputService
    {
        private readonly UIInput _input;

        public UiInputService()
        {
            _input = new UIInput();
            _input.Enable();
        }

       public float Jump()
       {
           return Mathf.Round(_input.Joystick.Jump.ReadValue<float>());
        }

        public float Shoot()
        {
            return 0f; //_input.Joystick.Shoot.ReadValue<float>();
        }

        public float Move()
        {
            return Mathf.Round(_input.Joystick.Move.ReadValue<float>());
        }

        public void Dispose()
        {
            _input.Disable();
            _input?.Dispose();
        }
    }
}
