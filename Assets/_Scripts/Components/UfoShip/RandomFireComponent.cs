using _Scripts.Components.Projectiles;
using UnityEngine;

namespace _Scripts.Components.UfoShip
{
    public class RandomFireComponent : MonoBehaviour
    {
        [SerializeField] private SpawnProjectileComponent _spawnProjectile;
        [SerializeField] private Transform _projectileSpawnPoint;
        [SerializeField] private float _spawnInterval;
        [SerializeField] private float _projectileLifetime;
        
        private float _elapsedSeconds = 0f;
        
        private void Update()
        {
            if (_elapsedSeconds >= _spawnInterval)
            {
                Fire();
                return;
            }

            _elapsedSeconds += Time.deltaTime;
        }

        private void Fire()
        {
            var randomDirection = new Vector2(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f));
                
            _elapsedSeconds = 0f;
            _spawnProjectile.Spawn(
                _projectileSpawnPoint.position,
                randomDirection,
                _projectileLifetime);
        }
    }
}