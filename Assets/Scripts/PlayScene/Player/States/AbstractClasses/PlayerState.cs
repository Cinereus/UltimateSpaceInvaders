using PlayScene.Common.StateMachine.Interfaces;

namespace PlayScene.Player.States.AbstractClasses
{
    public abstract class PlayerState : IState
    {
        protected PlayerController _player;

        protected PlayerState(PlayerController player)
        {
            _player = player;
        }
    
        public abstract void Enter();
        public abstract void Update();
        public abstract void PhysicsUpdate();
        public abstract void Exit();
    }
}
