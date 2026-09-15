using UnityEngine;

namespace BattleGridUnity.Scripts.Vehicles.Ground.Tracked
{
    [ExecuteAlways]
    public class WheelSurfaceGroundPoint : MonoBehaviour
    {
        [SerializeField]
        private float _wheelRadius;

        // [SerializeField]
        public Vector3 WheelGroundPoint { get; private set; }

        [SerializeField]
        private Vector3 _offset = Vector3.zero;



        // Game Loop Methods-----------------------------------------------------------------------

        private void Update()
        {
            WheelGroundPoint = transform.position + (_wheelRadius * Vector3.down) + _offset;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(WheelGroundPoint, 0.1f);
        }

        // Member Methods--------------------------------------------------------------------------
    }
}