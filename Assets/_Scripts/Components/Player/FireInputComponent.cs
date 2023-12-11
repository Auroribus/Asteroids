using _Scripts.Components.Projectiles;
using _Scripts.Components.Shared;
using UnityEngine;

namespace _Scripts.Components.Player
{
    public class FireInputComponent : TickComponent
    {
        [SerializeField] private SpawnProjectileComponent _spawnProjectile;
        [SerializeField] private Transform _projectileSpawnPoint;
        [SerializeField] private float _spawnInterval;
        [SerializeField] private float _projectileLifetime;
        
        private float _elapsedSeconds = 0f;
        private bool _canSpawn = true;

        public override void Tick()
        {
            if (_canSpawn)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    Fire();
                }
                return;
            }

            if (_elapsedSeconds >= _spawnInterval)
            {
                _canSpawn = true;
                return;
            }

            _elapsedSeconds += Time.deltaTime;
        }

        private void Fire()
        {
            _canSpawn = false;
            _elapsedSeconds = 0f;
            
            _spawnProjectile.Spawn(
                _projectileSpawnPoint.position,
                transform.up,
                _projectileLifetime);
        }
    }
}