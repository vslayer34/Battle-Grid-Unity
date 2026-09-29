using UnityEngine;
using UnityEngine.VFX;


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

        [SerializeField]
        protected VisualEffect _groundHit;



        // Game Loop Methods-----------------------------------------------------------------------

        private void OnEnable()
        {
            MoveForward();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == _groundLayerMask)
            {
                Debug.Log("Ground Hit");
                _groundHit.transform.SetParent(null);
                _groundHit.transform.position = collision.GetContact(0).point;
                _groundHit.Play();
            }
        }

        // Member Methods--------------------------------------------------------------------------

        protected virtual void MoveForward()
        {
            _rigidBody.AddForce(_projectileSpeed * transform.forward, ForceMode.Impulse);
        }
    }
}