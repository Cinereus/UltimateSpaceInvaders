namespace PlayScene.Common.StateMachine.Interfaces
{
    public interface IState
    {
        void Enter();
        void Update();
        void PhysicsUpdate();
        void Exit();
    }
}
