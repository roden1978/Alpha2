using System;
using UnityEngine;

namespace Services.Input
{
    public class UiInputService : IDisposable, IInputService
    {
        private readonly UIInput _input;
        public event Action OnJump;
        public event Action OnShoot;
        public UiInputService()
        {
            _input = new UIInput();
            _input.Enable();
            _input.Joystick.Jump.performed += _ => Jump();
        }

       public void Jump()
       {
           OnJump?.Invoke();
        }

        public void Shoot()
        {
            OnShoot?.Invoke();
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
