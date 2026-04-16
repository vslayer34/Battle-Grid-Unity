using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BattleGridUnity.Scripts.Input
{
    public class InputListener : MonoBehaviour
    {
        public event Action<Vector2> OnMoveInputsPressed;
        public event Action OnJumpInputPressed;
        public event Action<bool> OnCrouchToggled;

        private PlayerInputAction _playerInputAction;

        private InputAction _moveInputAction;
        private InputAction _jumpInputAction;
        private InputAction _crouchInputAction;

        private Vector2 _inputVector;

        private bool _isCrouchedToggleOn;



        // Game Loop Methods-----------------------------------------------------------------------

        private void Awake()
        {
            _playerInputAction = new PlayerInputAction();
            _playerInputAction.Enable();
            
            _moveInputAction = _playerInputAction.OnFoot.Move;
            _jumpInputAction = _playerInputAction.OnFoot.Jump;
            _crouchInputAction = _playerInputAction.OnFoot.Crouch;
        }

        private void Start()
        {
            _jumpInputAction.performed += OnJumpPressed;
            _crouchInputAction.performed += OnCrouchTogglePressed;
        }

        private void Update()
        {
            _inputVector = _moveInputAction.ReadValue<Vector2>();
            OnMoveInputsPressed?.Invoke(_inputVector);
        }


        private void OnDestroy()
        {
            _jumpInputAction.performed -= OnJumpPressed;
            _crouchInputAction.performed -= OnCrouchTogglePressed;

            _playerInputAction.Disable();
            _playerInputAction.Dispose();
        }

        // Member Methods--------------------------------------------------------------------------

        // Signal Methods--------------------------------------------------------------------------

        private void OnJumpPressed(InputAction.CallbackContext context) => OnJumpInputPressed?.Invoke();
        private void OnCrouchTogglePressed(InputAction.CallbackContext context) => OnCrouchToggled?.Invoke(_isCrouchedToggleOn = !_isCrouchedToggleOn);
    }
}