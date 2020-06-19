using UnityEngine;

namespace PlayScene.Player
{
    public class MoveLocker : MonoBehaviour
    {
        private Camera _camera;
        private const float OFFSET = 0.1f;
        private void Awake()
        {
            _camera = Camera.main;
        }

        private void FixedUpdate()
        {
            Vector3 bound;
            var position = transform.position;
            var viewport = _camera.WorldToViewportPoint(position);
            
            if (viewport.y > 1-OFFSET)
            {
                bound = _camera.ViewportToWorldPoint(new Vector3(0, 1-OFFSET, 0));
            }
            else if (viewport.y < OFFSET)
            {
                bound = _camera.ViewportToWorldPoint(new Vector3(0, OFFSET, 0));
            }
            else
            {
                return;
            }

            transform.position = new Vector3(position.x, bound.y, position.z);
        }
    }
}
