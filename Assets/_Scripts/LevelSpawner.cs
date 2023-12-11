using _Scripts.Components.Asteroids;
using _Scripts.Components.Pickups;
using _Scripts.Components.UfoShip;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts
{
    public class LevelSpawner : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private AsteroidComponent _asteroidPrefab;
        [SerializeField] private UfoShipComponent _ufoPrefab;

        [Header("Configs")]
        [SerializeField] private LevelConfig _config;
        
        private Camera _camera;
        
        public AsteroidComponent SpawnAsteroid(AsteroidSize size)
        {
            return SpawnAsteroid(size, GetRandomScreenPosition());
        }
        
        public AsteroidComponent SpawnAsteroid(AsteroidSize size, Vector2 position)
        {
            var asteroid = Instantiate(_asteroidPrefab, position, Quaternion.identity);
            asteroid.SetSize(size);
            return asteroid;
        }

        public UfoShipComponent SpawnUfo()
        {
            var ufo = Instantiate(_ufoPrefab, GetRandomScreenPosition(), Quaternion.identity);
            return ufo;
        }

        public PickupComponent SpawnRandomPickup()
        {
            var randomIndex = Random.Range(0, _config.Pickups.Length);
            var randomPickup = _config.Pickups[randomIndex];
            var pickup = Instantiate(randomPickup, GetRandomScreenPosition(), Quaternion.identity);
            return pickup;
        }
        
        private Vector2 GetRandomScreenPosition()
        {
            if (!_camera)
            {
                _camera = Camera.main;
            }

            if (!_camera)
            {
                _camera = Camera.current;
            }

            var screenSize = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
            var randomX = Random.Range(0f, screenSize.x);
            var randomY = Random.Range(0f, screenSize.y);

            return new Vector2(randomX, randomY);
        }
    }
}