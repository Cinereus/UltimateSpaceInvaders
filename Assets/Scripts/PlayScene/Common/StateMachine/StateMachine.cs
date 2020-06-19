using PlayScene.Common.StateMachine.Interfaces;

namespace PlayScene.Common.StateMachine
{
    public class StateMachine
    {
        private IState _currentState;

        public void ChangeState(IState state)
        {
            _currentState?.Exit();
        
            _currentState = state;
        
            _currentState?.Enter();
        }

        public void Update()
        {
            _currentState?.Update();
        }
    
        public void PhysicsUpdate()
        {
            _currentState?.PhysicsUpdate();
        }
    }
}
