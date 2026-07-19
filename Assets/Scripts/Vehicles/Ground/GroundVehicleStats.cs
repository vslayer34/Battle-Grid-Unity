using UnityEngine;



namespace BattleGridUnity.Scripts.Vehicles.Ground
{
    [CreateAssetMenu(fileName = "New GroundVehicle Stats", menuName = "Vehicle/Ground/VehicleStats")]
    public class GroundVehicleStats : ScriptableObject
    {
        [field: SerializeField]
        public float TurretRotationSpeed { get; private set; }

        [field: SerializeField]
        public float GunRotationSpeed { get; private set; }

        [field: SerializeField]
        public float MaxMainGunAngle { get; private set; }

        [field: SerializeField]
        public float MinMainGunAngle { get; private set; }
    }
}