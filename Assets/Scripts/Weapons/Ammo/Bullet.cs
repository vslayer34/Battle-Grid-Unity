using UnityEngine;

namespace BattleGridUnity.Scripts.Weapons.Ammo
{
    public class Bullet : Projectile
    {
        // Game Loop Methods-----------------------------------------------------------------------

        private void OnEnable()
        {
            MoveForward();
        }

        // Member Methods--------------------------------------------------------------------------

        protected override void MoveForward()
        {
            base.MoveForward();
        }
    }
}