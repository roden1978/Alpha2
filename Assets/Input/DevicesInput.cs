using UnityEngine;

namespace Input
{
    public class DevicesInput : MonoBehaviour
    {
        private PlayerInput _input;
        public float Direction { get; private set; }
        public float Jump { get; private set; }
        
        public float Shoot { get; private set; }


        private void Awake()
        {
            _input = new PlayerInput();
            _input.Player.Shoot.performed += _ => OnShoot();
            _input.Player.Shoot.canceled += _ => OnShoot();
            _input.Player.Jump.started += _ => OnJump();
            _input.Player.Jump.canceled += _ => OnJump();
        }

        private void OnEnable()
        {
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Disable();
        }

        private void OnJump()
        {
            Jump = _input.Player.Jump.ReadValue<float>();
        }

        private void OnShoot()
        {
            Shoot = _input.Player.Shoot.ReadValue<float>();
        }

        private void Update()
        {
            Direction = _input.Player.Move.ReadValue<float>();
        }
    }
}
