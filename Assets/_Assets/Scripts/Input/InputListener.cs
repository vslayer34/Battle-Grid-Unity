using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BattleGridUnity.Scripts.Input
{
    public class InputListener : MonoBehaviour
    {
        public event Action<Vector2> OnMoveInputsPressed;
        public event Action OnJumpInputPressed;
        private PlayerInputAction _playerInputAction;

        private InputAction _moveInputAction;
        private InputAction _jumpInputAction;

        private Vector2 _inputVector;



        // Game Loop Methods-----------------------------------------------------------------------

        private void Awake()
        {
            _playerInputAction = new PlayerInputAction();
            _playerInputAction.Enable();
            _moveInputAction = _playerInputAction.OnFoot.Move;
            _jumpInputAction = _playerInputAction.OnFoot.Jump;
        }

        private void Start()
        {
            _jumpInputAction.performed += OnJumpPressed;
        }

        private void Update()
        {
            _inputVector = _moveInputAction.ReadValue<Vector2>();
            OnMoveInputsPressed?.Invoke(_inputVector);
        }


        private void OnDestroy()
        {
            _jumpInputAction.performed -= OnJumpPressed;

            _playerInputAction.Disable();
            _playerInputAction.Dispose();
        }


        // Member Methods--------------------------------------------------------------------------

        // Signal Methods--------------------------------------------------------------------------

        private void OnJumpPressed(InputAction.CallbackContext context) => OnJumpInputPressed?.Invoke();
    }
}