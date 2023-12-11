using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Components.Asteroids;
using _Scripts.Components.Shared;
using _Scripts.Components.UfoShip;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Scripts
{
    public class LevelController : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Button _restartButton;
        [SerializeField] private TMP_Text _counterText;
        [SerializeField] private TMP_Text _controlsText;
        
        [Header("Player")]
        [SerializeField] private HealthComponent _player;
        [SerializeField] private ScoreController _score;

        [Header("Config")]
        [SerializeField] private LevelConfig _config;
        [Space]
        [SerializeField] private LevelSpawner _levelSpawner;
        [SerializeField] private ParticleSystem _explosionParticlesPrefab;
        
        private readonly List<HealthComponent> _objectives = new();
        private readonly WaitForSeconds _waitOneSecond = new(1f);
        
        private int _level = 1;

        private void Awake()
        {
            _restartButton.onClick.AddListener(() => SceneManager.LoadScene(0));
            _restartButton.transform.localScale = Vector3.zero;
        }

        private IEnumerator Start()
        {
            if (_player)
            {
                _player.OnDamage += OnDamagePlayer;
                _player.OnDeath += CheckGameOver;
            }
            
            _counterText.enabled = true;
            _counterText.text = "3";
            
            yield return _waitOneSecond;
            
            _counterText.text = "2";
            
            yield return _waitOneSecond;
            
            _counterText.text = "1";
            _controlsText.DOFade(0f, .5f);

            yield return _waitOneSecond;
            
            _counterText.text = "GO!";

            SpawnLevel();

            yield return _waitOneSecond;

            _counterText.enabled = false;
        }

        private void SpawnLevel()
        {
            for (var i = 0; i < _level + _config.AsteroidIncrementAmount; i++)
            {
                var asteroid = _levelSpawner.SpawnAsteroid(AsteroidSize.Large);   
                asteroid.Health.OnDeath += () => OnDestroyedAsteroid(asteroid);
                _objectives.Add(asteroid.Health);
            }

            if (_level > _config.UfoLevelThreshold)
            {
                var ufo = _levelSpawner.SpawnUfo();
                ufo.Health.OnDeath += () => OnDestroyedUfo(ufo);
                _objectives.Add(ufo.Health);
            }

            if (_level > _config.PickupLevelThreshold)
            {
                _levelSpawner.SpawnRandomPickup();
            }
        }

        private void OnDestroyedUfo(UfoShipComponent ufo)
        {
            PlayExplosionParticles(ufo.transform.position);
            _score.AddScore(_config.UfoScore);

            ufo.Health.OnDeath = null;
            _objectives.Remove(ufo.Health);
            
            TrySpawnNextLevel();
        }

        private void OnDestroyedAsteroid(AsteroidComponent asteroid)
        {
            PlayExplosionParticles(asteroid.transform.position);
            
            _score.AddScore(_config.AsteroidScore);
            
            asteroid.Health.OnDeath = null;
            _objectives.Remove(asteroid.Health);

            if (asteroid.Size == AsteroidSize.Small)
            {
                TrySpawnNextLevel();
                return;
            }

            var size = asteroid.Size - 1;
            for (var i = 0; i < _config.AsteroidSplitAmount; i++)
            {
                var smallerAsteroid = _levelSpawner.SpawnAsteroid(size, asteroid.transform.position);   
                smallerAsteroid.Health.OnDeath += () => OnDestroyedAsteroid(smallerAsteroid);
                _objectives.Add(smallerAsteroid.Health);
            }
        }

        private void TrySpawnNextLevel()
        {
            if (!_player.IsAlive)
            {
                return;
            }
            
            var canStartNextLevel = _objectives.Count <= 0 || _objectives.All(e => !e.IsAlive);
            if (canStartNextLevel)
            {
                ClearLevel();
                
                _level++;
                SpawnLevel();
            }
        }

        private void ClearLevel()
        {
            for (var i = 0; i < _objectives.Count; i++)
            {
                var objective = _objectives[i];
                if (!objective)
                {
                    continue;
                }
                
                objective.OnDeath -= TrySpawnNextLevel;
                
                Destroy(objective.gameObject);
            }
            
            _objectives.Clear();
        }

        private void OnDamagePlayer(int damage)
        {
            PlayExplosionParticles(_player.transform.position);
            _player.transform.position = Vector2.zero;
        }

        private void CheckGameOver()
        {
            if (!_player.IsAlive)
            {
                _counterText.text = "Game Over!\nPlayer Died!";
                
                DOTween.Sequence()
                    .AppendInterval(1f)
                    .Append(_restartButton.transform.DOScale(1f, .32f)
                        .SetEase(Ease.OutBack));
            }
        }

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveAllListeners();
        }

        private void PlayExplosionParticles(Vector3 position)
        {
            _explosionParticlesPrefab.transform.position = position;
            _explosionParticlesPrefab.Play();
        }
    }
}