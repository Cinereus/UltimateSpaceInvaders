using PlayScene.Common.AbstractClasses;
using UnityEngine;

namespace PlayScene.Common.Weapons
{
    public class Blaster : BaseWeapon
    {
        [SerializeField] private Vector3 _shotDirection = Vector3.right;
        [SerializeField] private float _distanceBetweenBullets = 0.4f;
        public override void Shoot()
        {
            for (var i = 0; i < _bulletsPerShot; i++)
            {
                var position = transform.position;
                var bullet = _bulletsPool.GetObject().GetComponent<Bullet>();
                
                bullet.Direction = _shotDirection;
                bullet.SetPosition(new Vector3(position.x+i*_distanceBetweenBullets, position.y, position.z));
            
                if (_renderer != null)
                {
                    bullet.Renderer.material = _renderer.material;
                }
            }
        }
    }
}
