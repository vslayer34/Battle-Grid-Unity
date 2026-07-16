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



        // Game Loop Methods-----------------------------------------------------------------------

        private void OnEnable()
        {
            _playerInputAction = new PlayerInputAction();
            _playerInputAction.Enable();

            _groundVehiclesAction = _playerInputAction.GroundVehicle;
            _switchCamera = _groundVehiclesAction.SwitchCamera;
        }

        private void Start()
        {
            _switchCamera.performed += OnSwitchCameraTogglePressed;
        }

        private void OnDisable()
        {
            _switchCamera.performed -= OnSwitchCameraTogglePressed;

            _playerInputAction.Disable();
            _playerInputAction.Dispose();
        }

        // Member Methods--------------------------------------------------------------------------

        private void OnSwitchCameraTogglePressed(InputAction.CallbackContext context) => OnCameraSwitchToggled?.Invoke();
    }
}