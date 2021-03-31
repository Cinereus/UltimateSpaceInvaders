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
        public float ShotTime { get; set; }
        public float MoveSpeed => _moveSpeed;
        public Animator Animator => _animator;
        public StateMachine StateMachine => _stateMachine;
        public WeaponSwitcher WeaponSwitcher => _weaponSwitcher;

        [SerializeField] private float _moveSpeed = 0.4f;
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _hitParticles;
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private WeaponSwitcher _weaponSwitcher;

        private const int PARTICLES_POOL_SIZE = 50;

        private float _shotTime;
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
            _stateMachine.ChangeState(new MovementState(this));
            _particlesPool = Pool.CreatePool(_hitParticles, PARTICLES_POOL_SIZE);
            OnDie += () => _gameOverPanel.SetActive(true);
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