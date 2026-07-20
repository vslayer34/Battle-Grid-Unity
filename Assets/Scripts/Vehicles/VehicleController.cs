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



        // Game Loop Methods-----------------------------------------------------------------------

        private void OnEnable()
        {
            
        }

        private void Update()
        {
            
        }

        private void OnDisable()
        {
            
        }

        // Member Methods--------------------------------------------------------------------------


        // Signal Methods--------------------------------------------------------------------------
    }
}