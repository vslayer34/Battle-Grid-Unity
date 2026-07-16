using System;
using System.Collections.Generic;
using BattleGridUnity.Scripts.Vehicles.Ground;
using Unity.Cinemachine;
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
        private GroundVehicleInput _groundInput;

        private CameraType _cameraType = CameraType.FpsCamera;

        [SerializeField, Header("Cameras")]
        private CinemachineBrain _cinemachineBrain;

        [SerializeField]
        private List<AvailableCameras> _availableCameras;



        // Game Loop Methods-----------------------------------------------------------------------

        private void OnEnable()
        {
            ActivateCamera();

            _groundInput.OnCameraSwitchToggled += SwitchCamera;
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

        // Signal Methods--------------------------------------------------------------------------

        private void SwitchCamera()
        {
            Debug.Log($"Camera switched");
            _cameraType = _cameraType == CameraType.FpsCamera ? CameraType.ThirdPersonCamera : CameraType.FpsCamera;
            ActivateCamera(_cameraType);
        }
    }
}