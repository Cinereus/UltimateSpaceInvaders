using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlayScene.Swarms
{
    public class Path : MonoBehaviour
    {
        public List<Vector3> CurvePoints => _curvePoints;
    
        [SerializeField] private Color _pathColor = Color.blue;
        [SerializeField] private Color _curveColor = Color.cyan;
        [SerializeField] private float _pathPointSize = 0.25f;
        [SerializeField] private float _curvePointSize = 0.1f;
        [SerializeField, Range(1, 15)] private int _curvePointsDensity;
        [SerializeField] private List<Vector3> _curvePoints = new List<Vector3>();
        [SerializeField] private List<Transform> _pathPoints = new List<Transform>();


        private void OnDrawGizmos()
        {
            Gizmos.color = _pathColor;
            _pathPoints.Clear();
            _pathPoints = GetComponentsInChildren<Transform>().ToList();
            _pathPoints.Remove(transform);

            Gizmos.DrawSphere(_pathPoints[0].position, _pathPointSize);

            for (var i = 1; i < _pathPoints.Count; i++)
            {
                var previousPoint = _pathPoints[i - 1].position;
                var currentPoint = _pathPoints[i].position;
                Gizmos.DrawLine(previousPoint, currentPoint);
                Gizmos.DrawWireSphere(currentPoint, _pathPointSize);
            }

            var extraPoints = 0;
        
            if (_pathPoints.Count % 2 == 0)
            {
                _pathPoints.Add(_pathPoints[_pathPoints.Count-1]);
                extraPoints = 2;
            }
            else
            {
                _pathPoints.Add(_pathPoints[_pathPoints.Count-1]);
                _pathPoints.Add(_pathPoints[_pathPoints.Count-1]);
                extraPoints = 3;
            }

            var startPoint = _pathPoints[0].position;
        
            Gizmos.color = _curveColor;
            _curvePoints.Clear();
        
            for (var i = 0; i < _pathPoints.Count-extraPoints; i += 2)
            {
                for (var j = 0; j <= _curvePointsDensity; j++)
                {
                    var point1 = _pathPoints[i].position;
                    var point2 = _pathPoints[i+1].position;
                    var point3 = _pathPoints[i+2].position;
                    var endPoint = GetCurvePoint(point1, point2, point3, j / Convert.ToSingle(_curvePointsDensity));
                    Gizmos.DrawLine(startPoint, endPoint);
                    Gizmos.DrawWireSphere(startPoint, _curvePointSize);
                    startPoint = endPoint;
                    _curvePoints.Add(startPoint);
                }
            }
        }

        private Vector3 GetCurvePoint(Vector3 point1, Vector3 point2, Vector3 point3, float time)
        {
            return Vector3.Lerp(Vector3.Lerp(point1, point2, time), 
                Vector3.Lerp(point2, point3, time), time);
        }
    }
}
