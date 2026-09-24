using UnityEngine;

namespace BattleGridUnity.Scripts.Vehicles
{
    public class VehicleAudioSources : MonoBehaviour
    {
        [SerializeField, Header("Main Gun")]
        private AudioSource _mainGunAudioSource;

        [SerializeField]
        private AudioClip _mainGunAudioClip;

        [SerializeField, Header("Secondary Gun")]
        private AudioSource _secondaryGunAudioSource;

        [SerializeField]
        private AudioClip _secondaryGunAudioClip;



        // Game Loop Methods-----------------------------------------------------------------------

        private void Start()
        {
            _mainGunAudioSource.clip = _mainGunAudioClip;
            _secondaryGunAudioSource.clip = _secondaryGunAudioClip;
        }

        // Member Methods--------------------------------------------------------------------------

        public void PlayMainGunSound() => _mainGunAudioSource.Play();
        public void PlaySecondaryGunSound() => _secondaryGunAudioSource.Play();
    }
}