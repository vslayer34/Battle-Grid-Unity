using System.Collections.Generic;
using UnityEngine;

namespace BattleGridUnity.Scripts.Vehicles
{
    public class TrackedVehicleController : VehicleController
    {
        [SerializeField]
        private List<WheelCollider> _rightSteeringWheels;

        [SerializeField]
        private List<WheelCollider> _lefttSteeringWheels;



        // Game Loop Methods-----------------------------------------------------------------------

        protected override void TurnVehicle(float steeringRange)
        {
            // Debug.Log($"Move in direction");

            // if (_groundInput.MovementInputVector.x >= 0.0f)
            // {
            //     foreach (var wheel in _rightSteeringWheels)
            //     {
            //         wheel.motorTorque = 0.0f;
            //         wheel.brakeTorque = 0.0f;
            //     }

            //     foreach (var wheel in _lefttSteeringWheels)
            //     {
            //         wheel.brakeTorque = 0.0f;
            //         wheel.motorTorque = _vehicleStats.MotorTorque * 4.0f;
            //     }
            // }
            // else if (_groundInput.MovementInputVector.x <= 0.0f)
            // {
            //     foreach (var wheel in _rightSteeringWheels)
            //     {
            //         wheel.brakeTorque = 0.0f;
            //         wheel.motorTorque = _vehicleStats.MotorTorque * 4.0f;
            //     }

            //     foreach (var wheel in _lefttSteeringWheels)
            //     {
            //         wheel.brakeTorque = 0.0f;
            //         wheel.motorTorque = 0.0f;
            //     }
            // }
        }
    }
}