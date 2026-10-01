using UnityEngine;
using UnityEngine.Pool;

namespace BattleGridUnity.Scripts.Utils
{
    public class GeneralObjectPool : MonoBehaviour
    {
        [SerializeField]
        protected int _poolDefaultSize;

        [SerializeField]
        protected int _poolMaxSize;

        [SerializeField]
        protected GameObject _poolObject;
        public ObjectPool<GameObject> GameObjectPool { get; protected set; }
    
    
    
        // Game Loop Methods---------------------------------------------------------------------------

        protected virtual void Awake()
        {
            // Create a pool with the four core callbacks.
            GameObjectPool = new ObjectPool<GameObject>(
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
        private GameObject CreateItem()
        {
            var poolObject = Instantiate(_poolObject, transform);
            poolObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            poolObject.SetActive(false);

            return poolObject;
        }

        // Called when an item is taken from the pool.
        private void OnGet(GameObject effect)
        {
            effect.SetActive(true);
        }

        // Called when an item is returned to the pool.
        private void OnRelease(GameObject effect)
        {
            effect.SetActive(false);
        }

        // Called when the pool decides to destroy an item (e.g., above max size).
        private void OnDestroyItem(GameObject effect)
        {
        }

        private System.Collections.IEnumerator ReturnAfter(GameObject pooledObject, float seconds)
        {
            yield return new WaitForSeconds(seconds);
            // Give it back to the pool.
            GameObjectPool.Release(pooledObject);
        }
    }
}