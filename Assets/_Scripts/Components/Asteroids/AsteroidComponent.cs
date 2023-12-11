using _Scripts.Components.Asteroids.Configs;
using _Scripts.Components.Shared;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Components.Asteroids
{
    public class AsteroidComponent : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private HealthComponent _health;
        [FormerlySerializedAs("_move")] [SerializeField] private RandomMoveComponent randomMove;
        
        [Header("References")]
        [SerializeField] private Transform _sizeTarget;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        [Header("Configs")]
        [SerializeField] private AsteroidsConfig _config;
        
        public AsteroidSize Size { get; private set; }
        public HealthComponent Health => _health;

        public void SetSize(AsteroidSize size)
        {
            Size = size;
            _sizeTarget.localScale = Vector3.one * _config.GetSizeFactor(size);
            _spriteRenderer.color = _config.GetColor(size);
            randomMove.IncreaseVelocity(_config.GetVelocityFactor(size));
        }
        
        private void Awake()
        {
            _health.OnDeath += OnDeath;
        }

        private void OnDeath()
        {
            gameObject.SetActive(false);
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