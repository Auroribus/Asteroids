using System;
using UnityEngine;

namespace _Scripts.Components.Shared
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _maxHealth;
        [SerializeField] private int _currentHealth;
        [SerializeField] private float _damageTimer;
        
        public int CurrentHealth => _currentHealth;
        public int MaxHealth => _maxHealth;
        
        public bool IsAlive => CurrentHealth > 0;

        public Action<int> OnDamage;
        public Action OnDeath;

        private bool _canTakeDamage;
        private float _elapsedDamageTimer;
        
        private void Awake()
        {
            ResetHealth();
        }

        public void Damage(int damage)
        {
            if (!_canTakeDamage)
            {
                return;
            }

            _elapsedDamageTimer = 0f;
            _canTakeDamage = false;
            
            _currentHealth -= damage;
            _currentHealth = Math.Clamp(_currentHealth, 0, _maxHealth);
            
            OnDamage?.Invoke(damage);
            CheckIsAlive();
        }

        public void ResetHealth()
        {
            _currentHealth = _maxHealth;
        }

        private void CheckIsAlive()
        {
            if (_currentHealth <= 0)
            {
                OnDeath?.Invoke();
            }
        }

        private void OnDestroy()
        {
            OnDamage = null;
            OnDeath = null;
        }

        private void Update()
        {
            if (_canTakeDamage)
            {
                return;
            }

            if (_elapsedDamageTimer >= _damageTimer)
            {
                _canTakeDamage = true;
                return;
            }
            
            _elapsedDamageTimer += Time.deltaTime;
        }
    }
}