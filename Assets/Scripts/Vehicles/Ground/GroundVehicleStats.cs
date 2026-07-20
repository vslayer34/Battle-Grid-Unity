using UnityEngine;



namespace BattleGridUnity.Scripts.Vehicles.Ground
{
    [CreateAssetMenu(fileName = "New GroundVehicle Stats", menuName = "Vehicle/Ground/VehicleStats")]
    public class GroundVehicleStats : ScriptableObject
    {
        [field: SerializeField, Header("Mobility")]
        public float TurretRotationSpeed { get; private set; }

        [field: SerializeField]
        public float HullRotationSpeed { get; private set; }

        [field: SerializeField]
        public float ForwardSpeed { get; private set; }

        [field: SerializeField]
        public float BackwardSpeed { get; private set; }


        [field: SerializeField, Header("Firepower")]
        public float GunRotationSpeed { get; private set; }

        [field: SerializeField]
        public float MaxElevationMainGunAngle { get; private set; }

        [field: SerializeField]
        public float MinDepressionMainGunAngle { get; private set; }

        
    }
}