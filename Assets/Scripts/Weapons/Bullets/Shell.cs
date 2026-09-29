using UnityEngine;


namespace BattleGridUnity.Scripts.Weapons.Ammo
{
    public class Shell : Projectile
    {
        // [SerializeField]
        // private float _recoilForce;

        [SerializeField]
        protected Rigidbody _rigidBody;

        [SerializeField]
        protected float _projectileSpeed = 10.0f;



        // Game Loop Methods-----------------------------------------------------------------------

        private void OnEnable()
        {
            MoveForward();
        }

        // Member Methods--------------------------------------------------------------------------

        protected virtual void MoveForward()
        {
            _rigidBody.AddForce(_projectileSpeed * transform.forward, ForceMode.Force);
        }
    }
}