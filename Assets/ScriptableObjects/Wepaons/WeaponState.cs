using System;
using UnityEngine;


namespace BattleGridUnity.ScriptableObjects.Wepaons
{
    [Serializable]
    public struct RateOfFireValues
    {
        [field: SerializeField]
        public int Min { get; private set; }

        [field: SerializeField]
        public int Max { get; private set; }
    }

    [CreateAssetMenu(fileName = "New Weapon Stats", menuName = "Weapons")]
    public class WeaponState : ScriptableObject
    {
        [field: SerializeField]
        public string WeaponName { get; private set; } = string.Empty;

        [field: SerializeField]
        public float Caliber { get; private set; }

        [field: SerializeField]
        public RateOfFireValues RateOfFire { get; private set; }

        [field: SerializeField, Space(5)]
        public int MainAmmoCapacity { get; private set; }

        [field: SerializeField]
        public int SecondaryAmmoCapacity { get; private set; } = -1;
    }
}