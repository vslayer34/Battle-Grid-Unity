using System.Collections;
using BattleGridUnity.ScriptableObjects.Wepaons;
using BattleGridUnity.Scripts.Vehicles;
using BattleGridUnity.Scripts.Weapons.Ammo;
using UnityEngine;

namespace BattleGridUnity.Scripts.Weapons
{
    public class VehicleAmmoSpawner : MonoBehaviour
    {
        [SerializeField]
        private VehicleWeapon _vehicleWeapon;

        [SerializeField]
        private Rigidbody _fireSource;

        [SerializeField]
        private Projectile _firedAmmo;


        [SerializeField]
        private WeaponState _weapon;




        // Game Loop Methods-----------------------------------------------------------------------

        private void Start()
        {
            _weapon = _vehicleWeapon.Weapon;
            _vehicleWeapon.OnWeaponFired += FireWeapon;
        }

        private void OnDestroy()
        {
            _vehicleWeapon.OnWeaponFired -= FireWeapon;
        }

        // Member Methods--------------------------------------------------------------------------


        // Check this thing it might work it might not
        private IEnumerator SetWeapon()
        {
            _weapon = _vehicleWeapon.Weapon;
            yield return new WaitUntil(() => _weapon != null);
        }

        public void AddRecoilForce(float recoilForce, Vector3 recoilDirection)
        {
            _fireSource.AddForce(recoilForce * recoilDirection, ForceMode.Impulse);
        }

        public void FireWeapon()
        {
            Debug.Log($"Weapon Fired {_weapon.WeaponName}");
        }
    }
}