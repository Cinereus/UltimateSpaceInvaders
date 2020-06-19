using System;
using PlayScene.Common.ObjectPool;
using UnityEngine;
using UnityEngine.Events;

namespace PlayScene.Common.AbstractClasses
{
    [RequireComponent(typeof(PoolObject))]
    public abstract class MortalEntity : MonoBehaviour
    {
        public UnityAction OnDie = () => {};
        public UnityAction<int> OnHealthChanged = arg0 => {};

        private int _defaultHealth;
        [SerializeField] protected int _healthPoints = 1;
        
        protected PoolObject _poolObject;

        protected virtual void Awake()
        {
            _defaultHealth = _healthPoints;
            _poolObject = GetComponent<PoolObject>();
        }

        public void TakeDamage(int damage)
        {
            _healthPoints -= _healthPoints >= damage ? damage : _healthPoints;
            OnHealthChanged(_healthPoints);

            if (_healthPoints != 0)
            {
                return;
            }
        
            Die();
        }

        protected virtual void Die()
        {
            OnDie();
            OnDie = () => { };
            _healthPoints = _defaultHealth;
            _poolObject.ReturnToPool();
        }
    }
}
