using System;
using System.Collections.Generic;
using PlayScene.Common.AbstractClasses;
using PlayScene.Common.ObjectPool;
using PlayScene.Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PlayScene.Common
{
    [RequireComponent(typeof(PoolObject))]
    public class Pickup : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 0.1f;

        private string _weaponName;
        private Renderer _renderer;
        private PoolObject _poolObject;
        
        private List<string> _weaponNames = new List<string>();
        private Dictionary<string, Material> _materials = new Dictionary<string, Material>();

        public void InitializeBonus(Vector3 position)
        {
            var randomIndex = Random.Range(0, _weaponNames.Count);
            _weaponName = _weaponNames[randomIndex];
            _renderer.material = _materials[_weaponName];
            transform.position = position;
        }
        
        private void Awake()
        {
            _poolObject = GetComponent<PoolObject>();
            var weaponSwitcher = FindObjectOfType<PlayerController>().WeaponSwitcher;
            var weapons = weaponSwitcher.GetComponentsInChildren<BaseWeapon>();
            
            foreach (var weapon in weapons)
            {
                var bullet = weapon.GetComponent<BaseWeapon>().Bullet.GetComponent<Bullet>();
                var material = bullet.GetComponent<Renderer>().sharedMaterial;
                
                _materials.Add(weapon.name, material);
                _weaponNames.Add(weapon.name);
            }
            
            _renderer = GetComponent<Renderer>();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void OnBecameInvisible()
        {
            _poolObject.ReturnToPool();
        }

        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                player.ChangeWeapon(_weaponName);
                _poolObject.ReturnToPool();
            }
        }
        
        private void Move()
        {
            transform.Translate(Vector3.left*_moveSpeed, Space.World);
        }
    }
}
