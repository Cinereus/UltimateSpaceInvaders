using PlayScene.Common.AbstractClasses;
using PlayScene.Common.ObjectPool;
using PlayScene.Invaders;
using PlayScene.Player;
using UnityEngine;

namespace PlayScene.Common
{
    [RequireComponent(typeof(PoolObject))]
    public class Bullet : MonoBehaviour
    {
        public bool IsEnemyBullet => _isEnemyBullet;
        
        public Renderer Renderer
        {
            get => _renderer;
            set => _renderer = value;
        }
        
        public Vector3 Direction
        {
            get => _direction;
            set => _direction = value;
        }
        
        [SerializeField] protected int _damage = 1;
        [SerializeField] protected float _speed = 0.4f;
        [SerializeField] protected bool _isEnemyBullet;
        [SerializeField] protected bool _isDestroyable;
        
        protected PoolObject _poolObject;

        private Vector3 _direction = Vector3.right;
        
        private Renderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _poolObject = GetComponent<PoolObject>();
        }

        private void FixedUpdate()
        {
            Move();
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
        
        private void Move()
        {
            transform.Translate(_direction*_speed, Space.World);
        }

        private void OnBecameInvisible()
        {
            _poolObject.ReturnToPool();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (_isDestroyable)
            {
                var bullet = other.GetComponent<Bullet>();

                if (bullet != null)
                {
                    if (bullet.IsEnemyBullet && _isEnemyBullet)
                    {
                        return;       
                    }
                    
                    _poolObject.ReturnToPool();
                }
            }
            
            MortalEntity entity;

            if (_isEnemyBullet)
            {
                entity = other.GetComponent<PlayerController>();
            }
            else
            {
                entity = other.GetComponent<Invader>();
            }

            
            if (entity == null)
            {
                return;
            }
        
            entity.TakeDamage(_damage);
            _poolObject.ReturnToPool();
        }

    }
}
