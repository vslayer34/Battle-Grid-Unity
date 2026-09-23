using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerInputAction;

namespace BattleGridUnity.Scripts.Vehicles.Ground
{
    public class GroundVehicleInput : MonoBehaviour
    {
        public event Action OnCameraSwitchToggled;
        public event Action<bool> OnPrimaryFireInteracted;
        public event Action<bool> OnSecondaryFireInteracted;


        private PlayerInputAction _playerInputAction;
        private GroundVehicleActions _groundVehiclesAction;


        private InputAction _switchCamera;
        private InputAction _lookInputAction;
        private InputAction _movementInputAction;
        private InputAction _primaryFireInputAction;
        private InputAction _secondaryFireInputAction;


        private Vector2 _lookInputVector;
        private Vector2 _movementInputVector;



        // Game Loop Methods-----------------------------------------------------------------------

        private void OnEnable()
        {
            _playerInputAction = new PlayerInputAction();
            _playerInputAction.Enable();

            _groundVehiclesAction = _playerInputAction.GroundVehicle;
            _switchCamera = _groundVehiclesAction.SwitchCamera;
            _lookInputAction = _groundVehiclesAction.Look;
            _movementInputAction = _groundVehiclesAction.BasicMovement;

            _primaryFireInputAction = _groundVehiclesAction.PrimaryFire;
            _secondaryFireInputAction = _groundVehiclesAction.SecondaryFire;
        }

        private void Start()
        {
            _switchCamera.performed += OnSwitchCameraTogglePressed;
            
            _primaryFireInputAction.performed += OnPrimaryFirePressed;
            _primaryFireInputAction.canceled += OnPrimaryFireReleased;

            _secondaryFireInputAction.performed += OnSecondaryFirePressed;
            _secondaryFireInputAction.canceled += OnSecondaryFireReleased;
        }

        private void Update()
        {
            _lookInputVector = _lookInputAction.ReadValue<Vector2>();
            _movementInputVector = _movementInputAction.ReadValue<Vector2>();
        }

        private void OnDisable()
        {
            _switchCamera.performed -= OnSwitchCameraTogglePressed;

            _primaryFireInputAction.performed += OnPrimaryFirePressed;
            _primaryFireInputAction.canceled += OnPrimaryFireReleased;

            _secondaryFireInputAction.performed += OnSecondaryFirePressed;
            _secondaryFireInputAction.canceled += OnSecondaryFireReleased;

            _playerInputAction.Disable();
            _playerInputAction.Dispose();
        }

        // Member Methods--------------------------------------------------------------------------

        private void OnSwitchCameraTogglePressed(InputAction.CallbackContext context) => OnCameraSwitchToggled?.Invoke();

        private void OnPrimaryFirePressed(InputAction.CallbackContext context) => OnPrimaryFireInteracted?.Invoke(true);
        private void OnPrimaryFireReleased(InputAction.CallbackContext context) => OnPrimaryFireInteracted?.Invoke(false);
        private void OnSecondaryFirePressed(InputAction.CallbackContext context) => OnSecondaryFireInteracted?.Invoke(true);
        private void OnSecondaryFireReleased(InputAction.CallbackContext context) => OnSecondaryFireInteracted?.Invoke(false);

        // Getters and Setters---------------------------------------------------------------------

        public Vector2 LookInputVector => _lookInputVector;
        public Vector2 MovementInputVector => _movementInputVector;
    }
}