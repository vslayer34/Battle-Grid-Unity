using BattleGridUnity.ScriptableObjects.Wepaons;
using BattleGridUnity.Scripts.Vehicles;
using UnityEngine;

namespace BattleGridUnity.Scripts.Weapons.Ammo
{
    public class Projectile : MonoBehaviour
    {
        [field: SerializeField]
        public WeaponState Stats { get; protected set; }

        [field: SerializeField]
        public VehicleAmmoSpawner FireSource { get; protected set; }

        [SerializeField]
        protected LayerMask _groundLayerMask;



        // Game Loop Methods-----------------------------------------------------------------------

        // Member Methods--------------------------------------------------------------------------

    }
}