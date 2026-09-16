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

        private int _numberOfIndecis;

        [SerializeField]
        private int _frameInterval = 5;

        private BezierKnot _knot;



        // Game Loop Methods-----------------------------------------------------------------------

        private void Start()
        {
            _spline = _trackSpline.Spline;
            _numberOfIndecis = _splineGroundPoints.Count;
        }

        private void Update()
        {
            // for (int i = 0; i < _splineGroundPoints.Count; i++)
            if (Time.frameCount % _frameInterval == 0)
            {
                UpdateSplinePointsLocationsToWheels(NumberOfIndecis);
                
                // UpdateSplinePointsLocationsToWheels(1);
                // UpdateSplinePointsLocationsToWheels(0);
                // UpdateSplinePointsLocationsToWheels(0);
            }
        }

        // Member Methods--------------------------------------------------------------------------

        private void UpdateSplinePointsLocationsToWheels(int index = 0)
        {
            // print($"Spline point 0 position {_trackSpline.transform.TransformPoint(_spline[_splineGroundPoints[0]].Position)}");
            // Vector3 knotWorldPosition = _trackSpline.transform.TransformPoint(_spline[_splineGroundPoints[0]].Position);

            Vector3 localKnotPosition = _trackSpline.transform.InverseTransformPoint(_corespondingWheels[index].WheelGroundPoint);


            _knot = _spline[_splineGroundPoints[index]];
            // var testKnot = _spline[index];

            _knot.Position = (float3)localKnotPosition;

            _spline.SetKnot(_splineGroundPoints[index], _knot);
            
            // for (int i = 0; i < _splineGroundPoints.Count; i++)
            // {
            //     _trackSpline.Spline[_splineGroundPoints[i]].Position
            // }
        }

        // Getters and Setters-------------------------------------------------------------------------

        public int NumberOfIndecis
        {
            get
            {
                if (_numberOfIndecis >= _splineGroundPoints.Count)
                {
                    _numberOfIndecis = 0;
                }
                return _numberOfIndecis++;
            }

        }
    }
}