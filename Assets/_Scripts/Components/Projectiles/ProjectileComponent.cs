using System;
using _Scripts.Components.Shared;
using UnityEngine;
using UnityEngine.Pool;

namespace _Scripts.Components.Projectiles
{
    public class ProjectileComponent : MonoBehaviour
    {
        [SerializeField] private HealthComponent _health;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _speed;

        private IObjectPool<ProjectileComponent> _pool;

        private float _elapsedSeconds;
        private float _lifeTime;
        private bool _isAlive;
        
        public void Setup(Vector3 position, Vector3 direction, float lifetime)
        {
            _health.ResetHealth();
            
            transform.position = position;

            _rigidbody2D.velocity = direction * _speed;

            _elapsedSeconds = 0f;
            _lifeTime = lifetime;
            _isAlive = true;
        }
        
        public void SetPool(IObjectPool<ProjectileComponent> pool)
        {
            _pool = pool;
        }

        private void Awake()
        {
            _health.OnDeath += OnDeath;
        }

        private void Update()
        {
            if (!_isAlive)
            {
                return;
            }

            if (_elapsedSeconds >= _lifeTime)
            {
                Release();
                return;
            }

            _elapsedSeconds += Time.deltaTime;
        }

        private void OnDeath()
        {
            Release();
        }

        private void Release()
        {
            _isAlive = false;
            _pool?.Release(this);
        }

        private void OnDestroy()
        {
            if (_health)
            {
                _health.OnDeath -= OnDeath;
            }
        }
    }
}