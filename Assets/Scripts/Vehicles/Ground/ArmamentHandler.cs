using System;
using System.Collections;
using BattleGridUnity.ScriptableObjects.Wepaons;
using UnityEngine;

namespace BattleGridUnity.Scripts.Vehicles.Ground
{
    public class ArmamentHandler : MonoBehaviour
    {
        public event Action<float> OnPrimaryWeaponFired;
        public event Action<float> OnSecondaryWeaponFired;
        
        [field: SerializeField]
        public GroundVehicleStats VehicleStats { get; private set; }

        [SerializeField]
        private GroundVehicleInput _groundInput;

        
        private WeaponState _mainGun;
        private WeaponState _secondaryGun;
        
        private bool _firingPrimaryArmament;
        private bool _firingSecondaryArmament;

        private Coroutine _primaryFireCoroutine;
        private Coroutine _secondaryFireCoroutine;



        // Game Loop Methods-----------------------------------------------------------------------

        private void Start()
        {
            _groundInput.OnPrimaryFireInteracted += SetPrimaryFireValue;
            _groundInput.OnSecondaryFireInteracted += SetSecondaryFireValue;

            _mainGun = VehicleStats.MainGun;
            _secondaryGun = VehicleStats.SecondaryGun;
        }

        private void OnDisable()
        {
            _groundInput.OnPrimaryFireInteracted -= SetPrimaryFireValue;
            _groundInput.OnSecondaryFireInteracted -= SetSecondaryFireValue;
        }

        // Member Methods--------------------------------------------------------------------------

        private IEnumerator FireMainArmament()
        {
            Debug.Log($"Firing Main Gun: {_mainGun.WeaponName}");
            float fireDelay = 60.0f / UnityEngine.Random.Range(_mainGun.RateOfFire.Min, _mainGun.RateOfFire.Max + 1);

            OnPrimaryWeaponFired?.Invoke(fireDelay);


            yield return new WaitForSeconds(fireDelay);

            if (_firingPrimaryArmament)
            {
                _primaryFireCoroutine = StartCoroutine(FireMainArmament());
            }
            else
            {
                _primaryFireCoroutine = null;
            }
        }

        private IEnumerator FireSecondaryArmament()
        {
            Debug.Log($"Firing Secondary Gun: {_secondaryGun.WeaponName}");

            float fireDelay = 60.0f / UnityEngine.Random.Range(_secondaryGun.RateOfFire.Min, _secondaryGun.RateOfFire.Max + 1);

            OnSecondaryWeaponFired?.Invoke(fireDelay);

            yield return new WaitForSeconds(fireDelay);

            if (_firingSecondaryArmament)
            {
                _secondaryFireCoroutine = StartCoroutine(FireSecondaryArmament());
            }
            else
            {
                _secondaryFireCoroutine = null;
            }
        }
        
        // Signal Methods--------------------------------------------------------------------------

        private void SetPrimaryFireValue(bool active)
        {
            _firingPrimaryArmament = active;

            if (_firingPrimaryArmament && _primaryFireCoroutine == null)
            {
                _primaryFireCoroutine = StartCoroutine(FireMainArmament());
            }
            // else
            // {
            //     if (_primaryFireCoroutine != null)
            //     {
            //         StopCoroutine(_primaryFireCoroutine);
            //         _primaryFireCoroutine = null;
            //     }
            // }
        }
        private void SetSecondaryFireValue(bool active)
        {
            _firingSecondaryArmament = active;

            if (_firingSecondaryArmament && _secondaryFireCoroutine == null)
            {
                _secondaryFireCoroutine = StartCoroutine(FireSecondaryArmament());
            }
            // else
            // {
            //     if (_secondaryFireCoroutine != null)
            //     {
            //         StopCoroutine(_secondaryFireCoroutine);
            //         _secondaryFireCoroutine = null;
            //     }
            // }
        }
    }
}