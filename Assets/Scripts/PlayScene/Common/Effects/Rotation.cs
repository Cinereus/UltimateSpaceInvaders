using UnityEngine;

namespace PlayScene.Common.Effects
{
    public class Rotation : MonoBehaviour
    {
        public void FixedUpdate()
        {
            transform.Rotate(Vector3.up * (Time.deltaTime * 100), Space.Self);
        }
    }
}
