using PlayScene.Common.AbstractClasses;
using PlayScene.Common.Effects;
using PlayScene.Common.ObjectPool;
using PlayScene.Common.StateMachine;
using PlayScene.Player.States;
using UnityEngine;

namespace PlayScene.Player
{
    public class PlayerController : MortalEntity
    {
        public float ShotTime
        {
            get => _shotTime;
            set => _shotTime = value;
        }
        
        public float MoveSpeed => _moveSpeed;
        
        public WeaponSwitcher WeaponSwitcher => _weaponSwitcher;
        public Animator Animator => _animator;
        public StateMachine StateMachine => _stateMachine;

        [SerializeField] private float _moveSpeed = 0.4f;
        [SerializeField] private GameObject _hitParticles;
        [SerializeField] private WeaponSwitcher _weaponSwitcher;
        
        private const int PARTICLES_POOL_SIZE = 50;

        private float _shotTime;
        private Animator _animator;
        private Pool _particlesPool;
        private StateMachine _stateMachine;

        public void ChangeWeapon(string weaponName)
        {
            _weaponSwitcher.ChangeWeapon(weaponName);
        }

        protected override void Awake()
        {
            base.Awake();
            _stateMachine = new StateMachine();
            _animator = GetComponentInChildren<Animator>();
            _stateMachine.ChangeState(new MovementState(this));
            _particlesPool = Pool.CreatePool(_hitParticles, PARTICLES_POOL_SIZE);
        }

        private void Update()
        {
            _stateMachine.Update();
        }

        private void FixedUpdate()
        {
            _stateMachine.PhysicsUpdate();
        }

        protected override void Die()
        {
            var particles = _particlesPool.GetObject().GetComponent<Particles>();

            particles.SetPosition(transform.position);
            particles.SetColor(Color.yellow);
            Cursor.visible = true;
            particles.Play();
            OnDie();
            Destroy(gameObject);
        }
    }
}
