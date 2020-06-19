using PlayScene.Player.States.AbstractClasses;
using UnityEngine;

namespace PlayScene.Player.States
{
    public class ShootingState : PlayerState
    {
        private WeaponSwitcher _weaponSwitcher;
        private Animator _animator;
        public ShootingState(PlayerController player) : base(player)
        {
            _weaponSwitcher = player.WeaponSwitcher;
            _animator = player.Animator;
        }
    
        public override void Enter()
        {
            Shoot();
            _player.StateMachine.ChangeState(new MovementState(_player));
        }

        public override void Update()
        {
        }

        public override void PhysicsUpdate()
        {
        }
    
        public override void Exit()
        {
        }
    
        private void Shoot()
        {
            var shotTime = _player.ShotTime;
            var buttonPressed = Input.GetMouseButton(0);
            var timeSinceLastShot = Time.timeSinceLevelLoad - shotTime;
            var cooldown = _weaponSwitcher.CurrentWeapon.Cooldown;
        
            if (buttonPressed && timeSinceLastShot >= cooldown)
            {
                _weaponSwitcher.Shoot();
                _player.ShotTime = Time.timeSinceLevelLoad;
                _animator.SetTrigger("Shoot");
            }
        }
    }
}
