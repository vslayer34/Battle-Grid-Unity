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



        // Game Loop Methods-----------------------------------------------------------------------

        private void Update()
        {
            WheelGroundPoint = transform.position + (_wheelRadius * Vector3.down);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(WheelGroundPoint, 0.1f);
        }

        // Member Methods--------------------------------------------------------------------------
    }
}