using System;
using System.Collections.Generic;
using BattleGridUnity.Scripts.Vehicles.Ground;
using Unity.Cinemachine;
using Unity.Mathematics;
using UnityEngine;

namespace BattleGridUnity.Scripts.Vehicles
{
    public class VehicleController : MonoBehaviour
    {
        [SerializeField]
        private GroundVehicleStats _vehicleStats;


        [SerializeField]
        private GroundVehicleInput _groundInput;


        [SerializeField, Header("Turret")]
        private Transform _turret;

        [SerializeField]
        private Transform _mantlet;

        [SerializeField]
        private Rigidbody _rigidBody;



        // Game Loop Methods-----------------------------------------------------------------------

        private void OnEnable()
        {
            
        }

        private void FixedUpdate()
        {
            MoveVehicleForward();
            TurnVehicle();
        }

        private void OnDisable()
        {
            
        }

        // Member Methods--------------------------------------------------------------------------

        private void MoveVehicleForward()
        {
            Debug.Log($"Forward Movement Vector {_groundInput.MovementInputVector.y}");

            float speed = 0.0f;


            if (_groundInput.MovementInputVector.y != 0.0f)
            {
                if (_groundInput.MovementInputVector.y > 0.0f)
                {
                    speed = _vehicleStats.ForwardSpeed;
                }
                else
                {
                    speed = -_vehicleStats.BackwardSpeed;
                }
                _rigidBody.AddRelativeForce(speed * Vector3.forward * Time.deltaTime, ForceMode.Acceleration);
            }
        }

        private void TurnVehicle()
        {
            Debug.Log($"Turn Movement Vector {_groundInput.MovementInputVector.x}");

            float turnSpeed = _groundInput.MovementInputVector.x * Time.deltaTime * _vehicleStats.HullRotationSpeed;

            // _rigidBody.MoveRotation(Quaternion.Euler(0.0f, turnSpeed, 0.0f));
            _rigidBody.AddRelativeTorque(0.0f, turnSpeed, 0.0f, ForceMode.Acceleration);
        }


        // Signal Methods--------------------------------------------------------------------------
    }
}