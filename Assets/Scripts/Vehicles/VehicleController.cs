using System;
using System.Collections.Generic;
using BattleGridUnity.Scripts.Vehicles.Ground;
using Unity.Cinemachine;
using Unity.Mathematics;
using UnityEngine;

namespace BattleGridUnity.Scripts.Vehicles
{
    [Serializable]
    internal struct WheelColliderAndMesh
    {
        [field: SerializeField]
        public WheelCollider Collider { get; private set; }

        [field: SerializeField]
        public Transform Mesh { get; private set; }

    }


    public class VehicleController : MonoBehaviour
    {
        [SerializeField]
        private GroundVehicleStats _vehicleStats;


        [SerializeField]
        private GroundVehicleInput _groundInput;

        [SerializeField, Header("Wheels")]
        private List<WheelColliderAndMesh> _vehicleWheels = new List<WheelColliderAndMesh>();

        [SerializeField]
        private List<WheelCollider> _driveWheels;

        [SerializeField]
        private List<WheelCollider> _steeringWheels;


        [SerializeField, Header("Turret")]
        private Transform _turret;

        [SerializeField]
        private Transform _mantlet;

        [SerializeField]
        private Rigidbody _rigidBody;

        private Vector3 _wheelPosition;
        private Quaternion _wheelRotation;



        // Game Loop Methods-----------------------------------------------------------------------

        private void Start()
        {
            Vector3 centerOfMass = _rigidBody.centerOfMass;
            centerOfMass.y += _vehicleStats.CenterOfGravityOffset;
            
            _rigidBody.centerOfMass = centerOfMass;
        }

        private void OnEnable()
        {
            
        }

        private void FixedUpdate()
        {
            MoveVehicleForward();
            // TurnVehicle();
        }

        private void OnDisable()
        {
            
        }

        // Member Methods--------------------------------------------------------------------------

        private void MoveVehicleForward()
        {
            Debug.Log($"Forward Movement Vector {_groundInput.MovementInputVector.y}");

            float speed = 0.0f;

            // Calculate current speed along the forward axis
            float forwardSpeed = Vector3.Dot(transform.forward, _rigidBody.linearVelocity);
            float speedFactor = Mathf.InverseLerp(0, _vehicleStats.MaxForwardSpeed, Mathf.Abs(forwardSpeed));

            // Reduce motor torque and steering at higher speeds
            float currentMotorTorque = Mathf.Lerp(_vehicleStats.MotorTorque, 0, speedFactor);
            float currentSteeringRange = Mathf.Lerp(_vehicleStats.SteeringRange, _vehicleStats.SteeringRangeAtMaxSpeed, speedFactor);

            bool isAccelerating = Mathf.Sign(_groundInput.MovementInputVector.y) == Mathf.Sign(forwardSpeed);

            // Calculate the vehicle steering
            TurnVehicle(currentSteeringRange);

            if (isAccelerating)
            {
                foreach (var wheel in _driveWheels)
                {
                    wheel.motorTorque = _groundInput.MovementInputVector.y * currentMotorTorque;

                    // disable brakes when accelerating
                    wheel.brakeTorque = 0.0f;
                }
            }
            else
            {
                foreach (var wheel in _driveWheels)
                {
                    wheel.motorTorque = 0.0f;

                    // disable brakes when accelerating
                    wheel.brakeTorque = Mathf.Abs(_groundInput.MovementInputVector.y * _vehicleStats.BrakeTorque);
                }
            }


            // if (_groundInput.MovementInputVector.y != 0.0f)
            // {
            //     if (_groundInput.MovementInputVector.y > 0.0f)
            //     {
            //         speed = _vehicleStats.MaxForwardSpeed;
            //     }
            //     else
            //     {
            //         speed = -_vehicleStats.BackwardSpeed;
            //     }
            //     _rigidBody.AddRelativeForce(speed * Vector3.forward * Time.deltaTime, ForceMode.Acceleration);
            // }

            UpdateWheelPositionAndRotation();

            Debug.Log($"Current Vehicle Speed: {forwardSpeed}");
        }

        private void UpdateWheelPositionAndRotation()
        {
            foreach (var wheel in _vehicleWheels)
            {
                wheel.Collider.GetWorldPose(out _wheelPosition, out _wheelRotation);
                wheel.Mesh.position = _wheelPosition;
                wheel.Mesh.rotation = _wheelRotation;
            }
        }

        private void TurnVehicle(float steeringRange)
        {
            // Calculate the vehicle steering
            foreach (var wheel in _steeringWheels)
            {
                if (_groundInput.MovementInputVector.x == 0.0f)
                {
                    wheel.steerAngle = Mathf.Lerp(wheel.steerAngle, 0.0f, Time.deltaTime);
                }
                else
                {
                    wheel.steerAngle += _groundInput.MovementInputVector.x * steeringRange * Time.deltaTime;
                    wheel.steerAngle = Mathf.Clamp(wheel.steerAngle, -_vehicleStats.SteeringRange, _vehicleStats.SteeringRange);
                }

                wheel.steerAngle = Mathf.Clamp(wheel.steerAngle, -_vehicleStats.SteeringRange, _vehicleStats.SteeringRange);
            }
        }


        // Signal Methods--------------------------------------------------------------------------
    }
}