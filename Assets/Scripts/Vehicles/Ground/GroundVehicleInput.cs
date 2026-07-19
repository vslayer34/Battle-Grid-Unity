using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerInputAction;

namespace BattleGridUnity.Scripts.Vehicles.Ground
{
    public class GroundVehicleInput : MonoBehaviour
    {
        public event Action OnCameraSwitchToggled;
        private PlayerInputAction _playerInputAction;
        private GroundVehicleActions _groundVehiclesAction;


        private InputAction _switchCamera;
        private InputAction _lookInputAction;
        private Vector2 _lookInputVector;



        // Game Loop Methods-----------------------------------------------------------------------

        private void OnEnable()
        {
            _playerInputAction = new PlayerInputAction();
            _playerInputAction.Enable();

            _groundVehiclesAction = _playerInputAction.GroundVehicle;
            _switchCamera = _groundVehiclesAction.SwitchCamera;
            _lookInputAction = _groundVehiclesAction.Look;
        }

        private void Start()
        {
            _switchCamera.performed += OnSwitchCameraTogglePressed;
        }

        private void Update()
        {
            _lookInputVector = _lookInputAction.ReadValue<Vector2>();
        }

        private void OnDisable()
        {
            _switchCamera.performed -= OnSwitchCameraTogglePressed;

            _playerInputAction.Disable();
            _playerInputAction.Dispose();
        }

        // Member Methods--------------------------------------------------------------------------

        private void OnSwitchCameraTogglePressed(InputAction.CallbackContext context) => OnCameraSwitchToggled?.Invoke();

        // Getters and Setters---------------------------------------------------------------------

        public Vector2 LookInputVector => _lookInputVector;
    }
}