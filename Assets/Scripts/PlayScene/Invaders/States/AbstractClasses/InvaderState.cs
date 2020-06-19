using PlayScene.Common.StateMachine.Interfaces;

namespace PlayScene.Invaders.States.AbstractClasses
{
    public abstract class InvaderState : IState
    {
        protected Invader _invader;

        protected InvaderState(Invader invader)
        {
            _invader = invader;
        }
    
        public abstract void Enter();
        public abstract void Update();
        public abstract void PhysicsUpdate();
        public abstract void Exit();
    }
}
