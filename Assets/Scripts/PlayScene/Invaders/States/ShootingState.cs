using PlayScene.Invaders.States.AbstractClasses;

namespace PlayScene.Invaders.States
{
    public class ShootingState : InvaderState
    {
        public ShootingState(Invader invader) : base(invader)
        {
        }
        
        public override void Enter()
        {
            _invader.Weapon.Shoot();
            _invader.StateMachine.ChangeState(new IdleState(_invader));
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

    }
}
