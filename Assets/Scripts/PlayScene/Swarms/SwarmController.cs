using UnityEngine;
using PlayScene.Invaders;
using System.Collections.Generic;
using System.Linq;
using PlayScene.Common.AbstractClasses;
using PlayScene.Common.ObjectPool;
using Random = UnityEngine.Random;

namespace PlayScene.Swarms
{
    public class SwarmController : MortalEntity
    {
        [SerializeField] private int _layersCount = 3;
        [SerializeField] private float _speed = 0.1f;
        [SerializeField] private float _topViewportPoint;
        [SerializeField] private float _leftViewportPoint;
        [SerializeField] private float _rightViewportPoint;
        [SerializeField] private float _bottomViewportPoint;
        
        private Camera _camera;
        private float _topBorder;
        private float _leftBorder;
        private float _rightBorder;
        private float _bottomBorder;
        private Vector3 _targetPosition;
        
        private GameObject[] _invaderPrefabs;
        private List<Path> _paths = new List<Path>();
        private List<Pool> _invaderPools = new List<Pool>();
        private List<string> _namesHolder = new List<string>();
        private List<Invader> _invaders = new List<Invader>();
        private List<Material> _invaderMaterials = new List<Material>();
        private List<FormationGrid[]> _swarmLayers = new List<FormationGrid[]>();
        
        private const int INVADERS_POOL_SIZE = 50;
        private const int SPAWN_DISTANCE = 3;

        public void InitializeSwarm()
        {
            foreach (var layer in _swarmLayers)
            {
                var path = GetUniqueRandomElement(_paths, _namesHolder);
                var invaderPool = GetUniqueRandomElement(_invaderPools, _namesHolder);
                var invaderMaterial = GetUniqueRandomElement(_invaderMaterials, _namesHolder);

                foreach (var formationGrid in layer)
                {
                    var formationPoints = formationGrid.Points;

                    for (var i = 0; i < formationPoints.Count; i++)
                    {
                        var point = formationPoints[i];
                        var invader = invaderPool.GetObject().GetComponent<Invader>();
                        var position = path.CurvePoints[0];

                        invader.Path = path;
                        invader.FormationPoint = point;
                        invader.SetMaterial(invaderMaterial);
                        invader.SetPosition(new Vector3(position.x+(i+SPAWN_DISTANCE), position.y, position.z));
                        invader.SetParent(formationGrid.transform);
                        _invaders.Add(invader);
                        SubscribeOnDieEvent(invader);
                    }
                }
            }
            
            _healthPoints = _invaders.Count;
            _namesHolder.Clear();
        }
        
        protected override void Awake()
        {
            base.Awake();
            _camera = Camera.main;
            _paths = FindObjectsOfType<Path>().ToList();
            _invaderPrefabs = Resources.LoadAll<GameObject>("Prefabs\\Invaders\\InvaderTypes");
            _invaderMaterials = Resources.LoadAll<Material>("Materials\\Invaders").ToList();
            _topBorder = _camera.ViewportToWorldPoint(new Vector3(0, _topViewportPoint, 0)).y;
            _leftBorder = _camera.ViewportToWorldPoint(new Vector3(1-_leftViewportPoint, 0, 0)).x;
            _rightBorder = _camera.ViewportToWorldPoint(new Vector3(_rightViewportPoint,0, 0)).x;
            _bottomBorder = _camera.ViewportToWorldPoint(new Vector3(0, 1-_bottomViewportPoint, 0)).y;
            
            foreach (var invader in _invaderPrefabs)
            {
                _invaderPools.Add(Pool.CreatePool(invader, INVADERS_POOL_SIZE));
            }
            
            for (var i = 0; i < _layersCount; i++)
            {
                var formation = transform.GetChild(i).GetComponentsInChildren<FormationGrid>();
                _swarmLayers.Add(formation);
            }
        }

        private void FixedUpdate()
        {
            if (CheckInvadersReadiness())
            {
                Move();
            }
        }

        private void SubscribeOnDieEvent(Invader invader)
        {
            invader.OnDie += () => TakeDamage(1);
        }

        protected override void Die()
        {
            foreach (var invader in _invaders)
            {
                invader.IsReady = false;
            }
            
            _invaders.Clear();
            base.Die();
        }


        private bool CheckInvadersReadiness()
        {
            foreach (var invader in _invaders)
            {
                if (!invader.IsReady)
                {
                    return false;
                }
            }

            return true;
        }

        private void Move()
        {
            if (Vector3.Distance(transform.position, _targetPosition) <= 0.0001f)
            {
                _targetPosition = GetRandomMovePoint();
            }
            
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed);
        }

        private Vector3 GetRandomMovePoint()
        {
            var randomX = Random.Range(_leftBorder, _rightBorder);
            var randomY = Random.Range(_bottomBorder, _topBorder);
            return new Vector3(randomX, randomY, 0);
        }
        
        private static T GetUniqueRandomElement<T>(List<T> list, List<string> namesHolder) where T : Object
        {
            for (;;)
            {
                var randomIndex = Random.Range(0, list.Count);
                var element = list[randomIndex];
         
                if(namesHolder.Contains(element.name))
                {
                    continue;
                }

                namesHolder.Add(element.name);
                return element;
            }
        }
    }
}
