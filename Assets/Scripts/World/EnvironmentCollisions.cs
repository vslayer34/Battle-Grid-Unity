using BattleGridUnity.Scripts.Weapons.Ammo;
using UnityEngine;

namespace BattleGridUnity.Scripts.World
{
    public class EnvironmentCollisions : MonoBehaviour
    {



        // Game Loop Methods-----------------------------------------------------------------------

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<Shell>(out Shell shell))
            {
                Debug.Log("Shell hit the ground");
            }
        }
    }
}