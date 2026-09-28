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
        protected Rigidbody _rigidBody;

        [SerializeField]
        protected float _projectileSpeed;



        // Game Loop Methods-----------------------------------------------------------------------

        // Member Methods--------------------------------------------------------------------------

        protected virtual void MoveForward()
        {
            _rigidBody.AddForce(_projectileSpeed * transform.forward, ForceMode.Force);
        }
    }
}