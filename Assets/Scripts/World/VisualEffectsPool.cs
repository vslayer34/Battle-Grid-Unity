using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.VFX;

namespace BattleGridUnity.Scripts.World
{
    public class VisualEffectsPool : MonoBehaviour
    {
        [SerializeField]
        private int _poolDefaultSize;

        [SerializeField]
        private int _poolMaxSize;

        [SerializeField]
        private VisualEffect _effect;
        private ObjectPool<VisualEffect> _effectsPool;
    
    
    
        // Game Loop Methods---------------------------------------------------------------------------

        private void Awake()
        {
            // Create a pool with the four core callbacks.
            _effectsPool = new ObjectPool<VisualEffect>(
                createFunc: CreateItem,
                actionOnGet: OnGet,
                actionOnRelease: OnRelease,
                actionOnDestroy: OnDestroyItem,
                collectionCheck: true,      // helps catch double-release mistakes
                defaultCapacity: _poolDefaultSize,
                maxSize: _poolMaxSize
            );
        }
    
        // Member Methods------------------------------------------------------------------------------

        // Creates a new pooled GameObject the first time (and whenever the pool needs more).
        private VisualEffect CreateItem()
        {
            var poolObject = Instantiate(_effect, transform);
            poolObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            poolObject.gameObject.SetActive(false);

            return poolObject;
        }

        // Called when an item is taken from the pool.
        private void OnGet(VisualEffect effect)
        {
            effect.gameObject.SetActive(true);
        }

        // Called when an item is returned to the pool.
        private void OnRelease(VisualEffect effect)
        {
            effect.gameObject.SetActive(false);
        }

        // Called when the pool decides to destroy an item (e.g., above max size).
        private void OnDestroyItem(VisualEffect effect)
        {
        }

        private System.Collections.IEnumerator ReturnAfter(VisualEffect pooledObject, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        // Give it back to the pool.
        _effectsPool.Release(pooledObject);
    }
    }
}