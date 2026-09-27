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

        [SerializeField]
        private bool _isThisGunRecoilable;

        [SerializeField]
        private Shell _shell;

        [SerializeField]
        private float _recoilForce;




        // Game Loop Methods-----------------------------------------------------------------------

        private void Start()
        {
            // _weapon = _vehicleWeapon.Weapon;

            StartCoroutine(SetWeapon());
            _vehicleWeapon.OnWeaponFired += FireWeapon;

            if (_isThisGunRecoilable)
            {
                _vehicleWeapon.OnWeaponFired += RecoilVehicle;
            }
        }

        private void OnDestroy()
        {
            _vehicleWeapon.OnWeaponFired -= FireWeapon;
            _vehicleWeapon.OnWeaponFired -= FireWeapon;
        }

        // Member Methods--------------------------------------------------------------------------


        // Check this thing it might work it might not
        private IEnumerator SetWeapon()
        {
            yield return new WaitUntil(() => _vehicleWeapon.Weapon != null);
            _weapon = _vehicleWeapon.Weapon;
        }

        private void AddRecoilForce(float recoilForce, Vector3 recoilDirection)
        {
            _fireSource.AddForce(recoilForce * recoilDirection, ForceMode.Impulse);
        }

        public void FireWeapon()
        {
            Debug.Log($"Weapon Fired {_weapon.WeaponName}");
        }

        public void RecoilVehicle()
        {
            AddRecoilForce(_recoilForce, -transform.forward);
        }
    }
}