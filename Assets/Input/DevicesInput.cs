using UnityEngine;

namespace Input
{
    public class DevicesInput : MonoBehaviour
    {
        private PlayerInput _input;
        private float _jump;
        public float Direction { get; private set; }
        public float Jump { get; private set; }


        private void Awake()
        {
            _input = new PlayerInput();
            _input.Player.Shoot.performed += ctx => OnSoot();
            _input.Player.Jump.performed += ctx => OnJump();
            _input.Player.Jump.canceled += ctx => OnStopJump();
        }

        private void OnStopJump()
        {
            Jump = _input.Player.Jump.ReadValue<float>();
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

        private void OnSoot()
        {
            Debug.Log("Soot");
        }

        private void Update()
        {
            Direction = _input.Player.Move.ReadValue<float>();
        }
    }
}
