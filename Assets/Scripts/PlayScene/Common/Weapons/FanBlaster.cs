using System.Collections.Generic;
using PlayScene.Common.AbstractClasses;
using UnityEngine;

namespace PlayScene.Common.Weapons
{
    public class FanBlaster : BaseWeapon
    {
        [SerializeField] private List<Vector3> _bulletDirections = new List<Vector3>();

        public override void Shoot()
        {
            for (var i = 0; i < _bulletDirections.Count; i++)
            {
                var bullet = _bulletsPool.GetObject().GetComponent<Bullet>();
                bullet.SetPosition(transform.position);
                bullet.Direction = _bulletDirections[i];
            
                if (_renderer != null)
                {
                    bullet.Renderer.material = _renderer.material;
                }
            }
        }
    }
}
