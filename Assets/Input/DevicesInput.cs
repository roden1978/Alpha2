using UnityEngine;

namespace Input
{
    public class DevicesInput : MonoBehaviour
    {
        private PlayerInput _input;
        private float _direction;
        private float _jump;

        private void Awake()
        {
            _input = new PlayerInput();
            _input.Player.Shoot.performed += ctx => OnSoot();
            _input.Player.Jump.performed += ctx => OnJump();
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
            _jump = _input.Player.Jump.ReadValue<float>();
            Debug.Log($"Jump {_jump}");
        }

        private void OnSoot()
        {
            Debug.Log("Soot");
        }

        private void Update()
        {
            _direction = _input.Player.Move.ReadValue<float>();
            Move(_direction);
        }

        private void Move(float direction)
        {
            Debug.Log($"Direction {direction}");
        }
    }
}
