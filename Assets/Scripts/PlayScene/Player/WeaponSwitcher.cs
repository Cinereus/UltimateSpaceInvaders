using System.Collections.Generic;
using System.Linq;
using PlayScene.Common.AbstractClasses;
using UnityEngine;

namespace PlayScene.Player
{
    public class WeaponSwitcher : MonoBehaviour
    {
        public BaseWeapon CurrentWeapon => _currentWeapon;
        
        [SerializeField] private BaseWeapon _currentWeapon;

        private Dictionary<string, BaseWeapon> _weaponMap = new Dictionary<string, BaseWeapon>();

        public void Awake()
        {
            var weapons = GetComponentsInChildren<BaseWeapon>().ToList();

            foreach (var weapon in weapons)
            { 
                _weaponMap.Add(weapon.name, weapon);
            }
        }

        public void ChangeWeapon(string weaponName)
        {
            _currentWeapon = _weaponMap[weaponName];
        }
    
        public void Shoot()
        {
            _currentWeapon.Shoot();
        }
    }
}
