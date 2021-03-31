using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using PlayScene.Common.AbstractClasses;

namespace PlayScene.Player
{
    public class WeaponSwitcher : MonoBehaviour
    {
        public BaseWeapon CurrentWeapon => _currentWeapon;
        
        [SerializeField] private BaseWeapon _currentWeapon;

        private Dictionary<string, BaseWeapon> _weaponMap = new Dictionary<string, BaseWeapon>();

        public void Awake()
        {
            _weaponMap = GetComponentsInChildren<BaseWeapon>().ToDictionary(w => w.name);
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
