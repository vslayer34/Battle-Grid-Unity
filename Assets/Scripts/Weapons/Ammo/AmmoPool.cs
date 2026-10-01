using BattleGridUnity.Scripts.Utils;
using UnityEngine;

namespace BattleGridUnity.Scripts.Weapons.Ammo
{
    public class AmmoPool : GeneralObjectPool
    {
        // Game Loop Methods---------------------------------------------------------------------------

        protected override void Awake()
        {
            base.Awake();
        }
    
        // Member Methods------------------------------------------------------------------------------

        public Projectile GetItem()
        {
            return GameObjectPool.Get().GetComponent<Projectile>();
        }

        public void ReleaseItem(Projectile effect)
        {
            GameObjectPool.Release(effect.gameObject);
        }
    }
}