using PlayScene.Invaders.States.AbstractClasses;
using UnityEngine;

namespace PlayScene.Invaders.States
{
    public class MovingToFormationState : InvaderState
    {
        public MovingToFormationState(Invader invader) : base(invader)
        {
        }

        public override void Enter()
        {
        }

        public override void Update()
        {
        }

        public override void PhysicsUpdate()
        {
            var invaderPosition = _invader.transform.position;
            var formationPoint = _invader.FormationPoint;
            
            if (Vector3.Distance(invaderPosition, formationPoint) <= 0.00001f)
            {
                _invader.StateMachine.ChangeState(new IdleState(_invader));
            }
            
            _invader.transform.position = Vector3.MoveTowards(invaderPosition, formationPoint, 0.1f);
        }

        public override void Exit()
        {
        }
    }
}
