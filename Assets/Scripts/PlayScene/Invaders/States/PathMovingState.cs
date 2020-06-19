using PlayScene.Invaders.States.AbstractClasses;
using PlayScene.Player;
using PlayScene.Player.States;
using UnityEngine;

namespace PlayScene.Invaders.States
{
    public class PathMovingState : InvaderState
    {
        private int _index;
        public PathMovingState(Invader invader) : base(invader)
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
            MoveToPath();
        }
    
        public override void Exit()
        {
        }
    
        private void MoveToPath()
        {
            var invaderPosition = _invader.transform.position;
            var curvePoints = _invader.Path.CurvePoints;
            
            if (Vector3.Distance(invaderPosition, curvePoints[_index]) <= 0.00001f)
            {
                if (_index == curvePoints.Count-1)
                {
                    _invader.StateMachine.ChangeState(new MovingToFormationState(_invader));
                }
                else
                {
                    _index++;
                }
            }
            
            _invader.transform.position = Vector3.MoveTowards(invaderPosition, curvePoints[_index], 0.3f);
        }
    }
}
