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
        public event Action<bool> OnProneToggled;

        private PlayerInputAction _playerInputAction;

        private InputAction _moveInputAction;
        private InputAction _jumpInputAction;
        private InputAction _crouchInputAction;
        private InputAction _proneInputAction;
        private InputAction _lookInputAction;

        private Vector2 _movementInputVector;
        private Vector2 _lookInputVector;

        private bool _isCrouchedToggleOn;
        private bool _isProneToggleOn;



        // Game Loop Methods-----------------------------------------------------------------------

        private void Awake()
        {
            _playerInputAction = new PlayerInputAction();
            _playerInputAction.Enable();
            
            _moveInputAction = _playerInputAction.OnFoot.Move;
            _jumpInputAction = _playerInputAction.OnFoot.Jump;
            _crouchInputAction = _playerInputAction.OnFoot.Crouch;
            _proneInputAction = _playerInputAction.OnFoot.Prone;
            _lookInputAction = _playerInputAction.OnFoot.Look;
        }

        private void Start()
        {
            _jumpInputAction.performed += OnJumpPressed;
            _crouchInputAction.performed += OnCrouchTogglePressed;
            _proneInputAction.performed += OnProneTogglePressed;
        }

        private void Update()
        {
            _movementInputVector = _moveInputAction.ReadValue<Vector2>();
            _lookInputVector = _lookInputAction.ReadValue<Vector2>();

            OnMoveInputsPressed?.Invoke(_movementInputVector);
        }


        private void OnDestroy()
        {
            _jumpInputAction.performed -= OnJumpPressed;
            _crouchInputAction.performed -= OnCrouchTogglePressed;
            _proneInputAction.performed -= OnProneTogglePressed;

            _playerInputAction.Disable();
            _playerInputAction.Dispose();
        }

        // Member Methods--------------------------------------------------------------------------

        // Signal Methods--------------------------------------------------------------------------

        private void OnJumpPressed(InputAction.CallbackContext context)
        {
            OnJumpInputPressed?.Invoke();
            _isCrouchedToggleOn = false;
            _isProneToggleOn = false;
        }

        private void OnCrouchTogglePressed(InputAction.CallbackContext context)
        {
            OnCrouchToggled?.Invoke(_isCrouchedToggleOn = !_isCrouchedToggleOn);

            if (_isCrouchedToggleOn)
            {
                _isProneToggleOn = false;
            }
        }

        private void OnProneTogglePressed(InputAction.CallbackContext context)
        {
            OnProneToggled?.Invoke(_isProneToggleOn = !_isProneToggleOn);

            if (_isProneToggleOn)
            {
                _isCrouchedToggleOn = false;
            }
        }

        // Getters & Setters-----------------------------------------------------------------------

        public Vector2 LookInputVector => _lookInputVector;
    }
}