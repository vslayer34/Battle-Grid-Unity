using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

namespace BattleGridUnity.Scripts.Vehicles.Ground.Tracked
{
    // [ExecuteAlways]
    public class SplineTracksHandler : MonoBehaviour
    {
        [SerializeField]
        private SplineContainer _trackSpline;

        [SerializeField]
        private List<int> _splineGroundPoints = new List<int>();

        [SerializeField]
        private List<WheelSurfaceGroundPoint> _corespondingWheels = new List<WheelSurfaceGroundPoint>();

        private Spline _spline;



        // Game Loop Methods-----------------------------------------------------------------------

        private void Start()
        {
            _spline = _trackSpline.Spline;
        }

        private void Update()
        {
            UpdateSplinePointsLocationsToWheels();
        }

        // Member Methods--------------------------------------------------------------------------

        private void UpdateSplinePointsLocationsToWheels()
        {
            // print($"Spline point 0 position {_trackSpline.transform.TransformPoint(_spline[_splineGroundPoints[0]].Position)}");
            // Vector3 knotWorldPosition = _trackSpline.transform.TransformPoint(_spline[_splineGroundPoints[0]].Position);

            Vector3 localKnotPosition = _trackSpline.transform.InverseTransformPoint(_corespondingWheels[0].WheelGroundPoint);

            var testKnot = _spline[_splineGroundPoints[0]];

            testKnot.Position = (float3)localKnotPosition;

            _spline.SetKnot(_splineGroundPoints[0], testKnot);
            
            // for (int i = 0; i < _splineGroundPoints.Count; i++)
            // {
            //     _trackSpline.Spline[_splineGroundPoints[i]].Position
            // }
        }
    }
}