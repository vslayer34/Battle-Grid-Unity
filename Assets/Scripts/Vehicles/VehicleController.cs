using System;
using System.Collections.Generic;
using BattleGridUnity.Scripts.Vehicles.Ground;
using Unity.Cinemachine;
using Unity.Mathematics;
using UnityEngine;

namespace BattleGridUnity.Scripts.Vehicles
{
    internal enum CameraType
    {
        FpsCamera,
        ThirdPersonCamera
    }

    [Serializable]
    internal struct AvailableCameras
    {
        [field: SerializeField]
        public CameraType Type { get; private set; }

        [field: SerializeField]
        public GameObject Camera { get; private set; }

        [field: SerializeField]
        public float BlendTime { get; private set; }
    }

    public class VehicleController : MonoBehaviour
    {
        [SerializeField]
        private GroundVehicleStats _vehicleStats;


        [SerializeField]
        private GroundVehicleInput _groundInput;

        private float _mouseSensitivity = 1.0f;

        private CameraType _cameraType = CameraType.FpsCamera;

        [SerializeField, Header("Cameras")]
        private CinemachineBrain _cinemachineBrain;

        [SerializeField]
        private List<AvailableCameras> _availableCameras;


        [SerializeField, Header("Turret")]
        private Transform _turret;

        [SerializeField]
        private Transform _mantlet;



        // Game Loop Methods-----------------------------------------------------------------------

        private void OnEnable()
        {
            ActivateCamera();
            Cursor.lockState = CursorLockMode.Locked;

            _groundInput.OnCameraSwitchToggled += SwitchCamera;
        }

        private void Update()
        {
            LookAround();
        }

        private void OnDisable()
        {
            _groundInput.OnCameraSwitchToggled -= SwitchCamera;
        }

        // Member Methods--------------------------------------------------------------------------

        private void ActivateCamera(CameraType type = CameraType.FpsCamera)
        {
            _cameraType = type;

            foreach (var item in _availableCameras)
            {
                if (type == item.Type)
                {
                    item.Camera.SetActive(true);
                    _cinemachineBrain.DefaultBlend.Time = item.BlendTime;
                }
                else
                {
                    item.Camera.SetActive(false);
                }
            }
        }

        private void LookAround()
        {
            
            float _turretRotation = Time.deltaTime * _groundInput.LookInputVector.x * _mouseSensitivity * _vehicleStats.TurretRotationSpeed;
            _turret.Rotate(Vector3.up, _turretRotation);

            float _gunRotation = _groundInput.LookInputVector.y * -1.0f * _mouseSensitivity * _vehicleStats.GunRotationSpeed * Time.deltaTime;
            float xRotation = Mathf.Clamp(_gunRotation * Mathf.Rad2Deg, -_vehicleStats.MaxMainGunAngle, _vehicleStats.MinMainGunAngle);

            if (_mantlet.localRotation.x * Mathf.Rad2Deg * 2.0f >= _vehicleStats.MinMainGunAngle)
            {
                _mantlet.localRotation = Quaternion.Euler(_vehicleStats.MinMainGunAngle, 0.0f, 0.0f);
            }
            else if (_mantlet.localRotation.x * Mathf.Rad2Deg * 2.0f <= -_vehicleStats.MaxMainGunAngle)
            {
                _mantlet.localRotation = Quaternion.Euler(-_vehicleStats.MaxMainGunAngle, 0.0f, 0.0f);
            }

            _mantlet.Rotate(Vector3.right, _gunRotation);
        }

        // Signal Methods--------------------------------------------------------------------------

        private void SwitchCamera()
        {
            Debug.Log($"Camera switched");
            _cameraType = _cameraType == CameraType.FpsCamera ? CameraType.ThirdPersonCamera : CameraType.FpsCamera;
            ActivateCamera(_cameraType);
        }
    }
}