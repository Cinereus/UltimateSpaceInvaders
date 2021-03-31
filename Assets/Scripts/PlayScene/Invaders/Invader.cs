using UnityEngine;
using PlayScene.UI;
using PlayScene.Common;
using PlayScene.Common.Effects;
using PlayScene.Invaders.States;
using PlayScene.Common.StateMachine;
using PlayScene.Common.AbstractClasses;
using PlayScene.Common.ObjectPool;
using PlayScene.Swarms;

namespace PlayScene.Invaders
{
    public class Invader : MortalEntity
    {
        public BaseWeapon Weapon => _weapon;
        public float MinShotCooldown => _minShotCooldown;
        public float MaxShotCooldown => _maxShotCooldown;
        public StateMachine StateMachine => _stateMachine;
        
        public Path Path
        {
            get => _path;
            set => _path = value;
        }
        
        public float ShotTime
        {
            get => _shotTime;
            set => _shotTime = value;
        }
        
        public Vector3 FormationPoint
        {
            get => _formationPoint;
            set => _formationPoint = value;
        }
        
        public bool IsReady
        {
            get => _isReady;
            set => _isReady = value;
        }
        
        [SerializeField] private int _scores = 2;
        [SerializeField] private float _minShotCooldown = 1f;
        [SerializeField] private float _maxShotCooldown = 6f;
        [SerializeField] private ScoreText[] _scoreTextList;
        [SerializeField, Range(1, 100)] private int _bonusChance = 5;
        [SerializeField] private GameObject _hitParticles;
        [SerializeField] private GameObject _pickup;
        
        private bool _isReady;
        private float _shotTime;
        private Path _path;
        private Pool _particlesPool;
        private Pool _pickupsPool;
        private Renderer _renderer; 
        private BaseWeapon _weapon;
        private Vector3 _formationPoint;
        private StateMachine _stateMachine;
        
        
        private const int PICKUP_POOL_SIZE = 15;
        private const int PARTICLES_POOL_SIZE = 50;

        public void SetParent(Transform parent)
        {
            transform.parent = parent;
        }
        
        public void SetMaterial(Material material)
        {
            _renderer.material = material;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        protected override void Die()
        {
            _formationPoint = Vector3.zero;
            _shotTime = 0;
            _isReady = true;
            _path = null;
            
            var particles = _particlesPool.GetObject().GetComponent<Particles>();
            
            particles.SetPosition(transform.position);
            particles.SetColor(_renderer.material.color);
            particles.Play();
            GetPickup();
            
            foreach (var scoreText in _scoreTextList)
            {
                scoreText.GainScore(_scores);
            }
            
            base.Die();
        }

        protected override void Awake()
        {
            base.Awake();
            _stateMachine = new StateMachine();
            _renderer = GetComponent<Renderer>();
            _weapon = GetComponentInChildren<BaseWeapon>();
            _pickupsPool = Pool.CreatePool(_pickup, PICKUP_POOL_SIZE);
            _particlesPool = Pool.CreatePool(_hitParticles, PARTICLES_POOL_SIZE);
        }

        private void OnEnable()
        {
            _isReady = false;
            _shotTime = 0;
            _stateMachine.ChangeState(new PathMovingState(this));
        }

        private void Update()
        {
            _stateMachine.Update();
        }

        private void FixedUpdate()
        {
            _stateMachine.PhysicsUpdate();
        }

        private void GetPickup()
        {
            var attempt = Random.Range(1, 100);

            if (attempt <= _bonusChance)
            {
                _pickupsPool.GetObject().GetComponent<Pickup>().InitializeBonus(transform.position);
            }
        }
    }
}
