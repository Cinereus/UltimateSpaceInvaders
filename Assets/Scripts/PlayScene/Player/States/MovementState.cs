using PlayScene.Player.States.AbstractClasses;
using UnityEngine;

namespace PlayScene.Player.States
{
    public class MovementState : PlayerState
    {
        private Camera _camera;
    
        public MovementState(PlayerController player) : base(player)
        {
            _camera = Camera.main;
        }

        public override void Enter()
        {
        }

        public override void Update()
        {
            if (Input.GetMouseButton(0))
            {
                _player.StateMachine.ChangeState(new ShootingState(_player));     
            }
        }

        public override void PhysicsUpdate()
        {
            Move();
        }

        public override void Exit()
        {
        }
    
        private void Move()
        {
            var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            var position = _player.transform.position;
            _player.transform.position = Vector3.MoveTowards(position, new Vector3(position.x, mousePosition.y, position.z), _player.MoveSpeed);
        }
    }
}
