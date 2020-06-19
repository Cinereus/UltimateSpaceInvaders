using PlayScene.Common.ObjectPool;
using UnityEngine;

namespace PlayScene.Common.Effects
{
    [RequireComponent(typeof(ParticleSystem), typeof(PoolObject))]
    public class Particles : MonoBehaviour
    {
        private Renderer _renderer;
        private PoolObject _poolObject;
        private ParticleSystem _particles;

        private void Awake()
        {
            _poolObject = GetComponent<PoolObject>();
            _particles = GetComponent<ParticleSystem>();
            _renderer = _particles.GetComponent<Renderer>();
        }

        public void SetColor(Color color)
        {
            _renderer.material.color = color;
        }
    
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void Play()
        {
            _particles.Play();
        }

        private void Update()
        {
            if (!_particles.IsAlive())
            { 
                _poolObject.ReturnToPool();
            }
        }
    }
}
