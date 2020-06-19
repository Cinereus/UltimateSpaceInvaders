using PlayScene.Common.ObjectPool;
using UnityEngine;

namespace PlayScene.Common.AbstractClasses
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        public float Cooldown => _cooldown;
        public GameObject Bullet => _bullet;

        [SerializeField] protected Renderer _renderer;
        [SerializeField] protected GameObject _bullet;
        [SerializeField] protected int _bulletCount = 200;
        [SerializeField] protected int _bulletsPerShot = 3;
        [SerializeField] protected float _cooldown ;

        protected Pool _bulletsPool;

        private void OnValidate()
        {
            var bullet = _bullet.GetComponent<Bullet>();
        
            if (bullet == null)
            {
                _bullet = null;
                Debug.LogWarning("You use game object without Bullet component!");
            }
        }

        protected virtual void Awake()
        {
            _bulletsPool = Pool.CreatePool(_bullet, _bulletCount);
        }

        public abstract void Shoot();
    }
}
