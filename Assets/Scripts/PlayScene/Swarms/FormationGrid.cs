using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PlayScene.Swarms
{
    public class FormationGrid : MonoBehaviour
    {
        public List<Vector3> Points => _points;
    
        [SerializeField] private int _gridSizeY = 10;
        [SerializeField] private int _gridSizeX = 2;
        [SerializeField] private float _offsetX = 1f;
        [SerializeField] private float _offsetY = 1f;
        private List<Vector3> _points = new List<Vector3>();


        private void Awake()
        {
            for (var i = 0; i < _gridSizeX; i++)
            {
                for (var j = 0; j < _gridSizeY; j++)
                {
                    var position = transform.position;
                    var newX = position.x + _offsetX*i;
                    var newY = position.y + _offsetY*j;
                    var formationPoint = new Vector3(newX, newY, 0);
                
                    _points.Add(formationPoint);
                }   
            }
        }

        private void OnDrawGizmos()
        {
            DrawPoints();
        }

        private void DrawPoints()
        {
            var num = 0;
        
            for (var i = 0; i < _gridSizeX; i++)
            {
                for (var j = 0; j < _gridSizeY; j++)
                {
                    var position = transform.position;
                    var newX = position.x + _offsetX*i;
                    var newY = position.y + _offsetY*j;
                    var formationPoint = new Vector3(newX, newY, 0);
                
                    Handles.Label(formationPoint, num.ToString());
                    num++;
                }   
            }
        }
    }
}
