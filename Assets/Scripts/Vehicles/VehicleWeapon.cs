using System;
using System.Collections;
using BattleGridUnity.Scripts.Vehicles.Ground;
using DG.Tweening;
using UnityEngine;

namespace BattleGridUnity.Scripts.Vehicles
{
    enum VehicleWeaponClass
    {
        Primary,
        Secondary
    }

    public class VehicleWeapon : MonoBehaviour
    {
        [SerializeField, Header("Armament Handler")]
        private ArmamentHandler _armamentHandler;

        [SerializeField]
        private VehicleWeaponClass _weaponClass;


        [SerializeField, Header("Audio")]
        private AudioSource _gunAudioSource;

        [SerializeField]
        private AudioClip _gunAudioCLip;

        [SerializeField, Header("Mesh")]
        private Transform _weaponMesh;

        [SerializeField]
        private Transform _weaponRecoilPoint;

        [SerializeField]
        private float _weaponRecoilTime;

        private Vector3 _orignalGunPosition;



        // Game Loop Methods-----------------------------------------------------------------------

        private void Start()
        {
            _gunAudioSource.clip = _gunAudioCLip;

            if (_weaponMesh)
            {
                _orignalGunPosition = _weaponMesh.localPosition;
            }
            // _weaponRecoilTime = _armamentHandler.VehicleStats.


            if (_weaponClass == VehicleWeaponClass.Primary)
            {
                _armamentHandler.OnPrimaryWeaponFired += FireWeapon;
            }
            else if (_weaponClass == VehicleWeaponClass.Secondary)
            {
                _armamentHandler.OnSecondaryWeaponFired += FireWeapon;
            }
        }

        private void OnDestroy()
        {
            if (_weaponClass == VehicleWeaponClass.Primary)
            {
                _armamentHandler.OnPrimaryWeaponFired -= FireWeapon;
            }
            else if (_weaponClass == VehicleWeaponClass.Secondary)
            {
                _armamentHandler.OnSecondaryWeaponFired -= FireWeapon;
            }
        }

        // Member Methods--------------------------------------------------------------------------

        private void PlayAudio() => _gunAudioSource.Play();
        // private void RecoilGun(float fireDelay)
        // {
        //     _weaponMesh.DOLocalMove(_weaponRecoilPoint.localPosition, fireDelay / 2.0f).OnComplete(() =>
        //     {
        //         _weaponMesh.DOLocalMove(_orignalGunPosition, fireDelay / 2.0f);
        //     });
        // }

        private IEnumerator RecoilGun(float fireDelay)
        {
            float elapsedTime = 0.0f;

            while (elapsedTime <= fireDelay / 2.0f)
            {
                elapsedTime += Time.deltaTime;

                _weaponMesh.localPosition = Vector3.Lerp(_orignalGunPosition, _weaponRecoilPoint.localPosition, elapsedTime / (fireDelay / 2.0f));

                yield return null;
            }

            _weaponMesh.localPosition = _weaponRecoilPoint.localPosition;

            elapsedTime = 0.0f;

            while (elapsedTime <= fireDelay / 2.0f)
            {
                elapsedTime += Time.deltaTime;

                _weaponMesh.localPosition = Vector3.Lerp(_weaponRecoilPoint.localPosition, _orignalGunPosition, elapsedTime / (fireDelay / 2.0f));

                yield return null;
            }

            _weaponMesh.localPosition = _orignalGunPosition;
        }

        // Signal Methods--------------------------------------------------------------------------

        private void FireWeapon(float fireDelay)
        {
            PlayAudio();

            if (_weaponMesh == null)
            {
                return;
            }

            StartCoroutine(RecoilGun(fireDelay));
        }
    }
}