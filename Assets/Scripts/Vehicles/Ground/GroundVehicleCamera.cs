using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

namespace BattleGridUnity.Scripts.Vehicles.Ground
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
    
    public class GroundVehicleCamera : MonoBehaviour
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
        private Transform _cameraLookTarget;

        [SerializeField]
        private float _cameraRotationSpeed = 10.0f;

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
            
            float _cameraRotation = Time.deltaTime * _groundInput.LookInputVector.x * _mouseSensitivity * _cameraRotationSpeed;
            _cameraLookTarget.Rotate(Vector3.up, _cameraRotation);
            // _turret.localRotation *= Quaternion.Euler(0.0f, _turretRotation, 0.0f);
            
            // if (_groundInput.LookInputVector.x != 0.0f)
            // {
            //     float direction = _groundInput.LookInputVector.x > 0 ? 1 : -1;
            //     float turretRotaion = Time.deltaTime * direction * _vehicleStats.TurretRotationSpeed;

            //     if (_cameraType == CameraType.FpsCamera)
            //     {
            //         _turret.localRotation *= Quaternion.Euler(0.0f, turretRotaion, 0.0f);
            //     }
            // }
            if (_cameraType == CameraType.FpsCamera && _groundInput.LookInputVector.x != 0.0f)
            {
                float direction = _groundInput.LookInputVector.x > 0 ? 1 : -1;
                float turretRotaion = Time.deltaTime * direction * _vehicleStats.TurretRotationSpeed;

                _turret.localRotation *= Quaternion.Euler(0.0f, turretRotaion, 0.0f);
            }
            else if (_cameraType == CameraType.ThirdPersonCamera)
            {
                _turret.localRotation = Quaternion.RotateTowards(_turret.localRotation, _cameraLookTarget.localRotation, Time.deltaTime * _vehicleStats.TurretRotationSpeed);
            }

            float _gunRotation = _groundInput.LookInputVector.y * -1.0f * _mouseSensitivity * _vehicleStats.GunRotationSpeed * Time.deltaTime;
            float xRotation = Mathf.Clamp(_gunRotation * Mathf.Rad2Deg, -_vehicleStats.MaxElevationMainGunAngle, _vehicleStats.MinDepressionMainGunAngle);

            if (_mantlet.localRotation.x * Mathf.Rad2Deg * 2.0f >= _vehicleStats.MinDepressionMainGunAngle)
            {
                _mantlet.localRotation = Quaternion.Euler(_vehicleStats.MinDepressionMainGunAngle, 0.0f, 0.0f);
            }
            else if (_mantlet.localRotation.x * Mathf.Rad2Deg * 2.0f <= -_vehicleStats.MaxElevationMainGunAngle)
            {
                _mantlet.localRotation = Quaternion.Euler(-_vehicleStats.MaxElevationMainGunAngle, 0.0f, 0.0f);
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