using System.Threading.Tasks;
using BattleGridUnity.Scripts.Weapons.Ammo;
using UnityEngine;
using UnityEngine.VFX;

namespace BattleGridUnity.Scripts.World
{
    public class EnvironmentCollisions : MonoBehaviour
    {
        [SerializeField]
        private VisualEffectsPool _effectsPool;



        // Game Loop Methods-----------------------------------------------------------------------

        private async void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<Shell>(out Shell shell))
            {
                Debug.Log("Shell hit the ground");
                var activeEffect = _effectsPool.GetItem();
                activeEffect.transform.position = collision.contacts[0].point;
                activeEffect.Play();

                await Awaitable.WaitForSecondsAsync(5.0f);

                _effectsPool.ReleaseItem(activeEffect);
            }
        }
    }
}