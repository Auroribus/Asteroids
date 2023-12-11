using _Scripts.Components.Shared;
using UnityEngine;

namespace _Scripts.Components.UfoShip
{
    public class UfoShipComponent : MonoBehaviour
    {
        [SerializeField] private HealthComponent _health;

        public HealthComponent Health => _health;

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