using PlayScene.Invaders.States.AbstractClasses;
using UnityEngine;

namespace PlayScene.Invaders.States
{
    public class IdleState : InvaderState
    {
        private float _cooldown;
        public IdleState(Invader invader) : base(invader)
        {
            _cooldown = Random.Range(invader.MinShotCooldown, invader.MaxShotCooldown + 1f);
        }

        public override void Enter()
        {
            _invader.IsReady = true;
            _invader.ShotTime = Time.timeSinceLevelLoad;
        }

        public override void Update()
        {
            var shotTime = _invader.ShotTime;
            var timeSinceLastShot = Time.timeSinceLevelLoad - shotTime;
        
            if (timeSinceLastShot >= _cooldown)
            {
                _invader.ShotTime = Time.timeSinceLevelLoad;
                _invader.StateMachine.ChangeState(new ShootingState(_invader));
            }
        }

        public override void PhysicsUpdate()
        {
        }

        public override void Exit()
        {
        }
    }
}
