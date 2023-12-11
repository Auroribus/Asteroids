using _Scripts.Components.Shared;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Components.Player
{
    public class PlayerComponent : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Image[] _healthImages;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        [Header("Components")]
        [SerializeField] private HealthComponent _health;
        [SerializeField] private MoveInputComponent _moveInputComponent;
        [SerializeField] private TickComponent[] _tickComponents;

        private Tween _damageTween;
        
        private void Awake()
        {
            _health.OnDamage += OnDamage;
            _health.OnDeath += OnDeath;
            _damageTween = CreateDamageTween();
        }

        private void OnDestroy()
        {
            if (_health)
            {
                _health.OnDamage -= OnDamage;
            }
        }

        private void OnDamage(int damage)
        {
            _moveInputComponent.ResetVelocity();
            _damageTween.Restart();

            for (var i = 0; i < _health.MaxHealth; i++)
            {
                var isEnabled = _health.CurrentHealth > i;

                if (!isEnabled)
                {
                    var target = _healthImages[i];
                    PlayDamageAnimation(target);
                    continue;
                }
                
                _healthImages[i].enabled = true;
            }
        }

        private void OnDeath()
        {
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (!_health.IsAlive)
            {
                return;
            }

            foreach (var component in _tickComponents)
            {
                component.Tick();
            }
        }

        private Tween CreateDamageTween()
        {
            return DOTween.Sequence()
                .Append(_spriteRenderer.DOFade(.25f, .25f))
                .Append(_spriteRenderer.DOFade(1f, .25f))
                .Append(_spriteRenderer.DOFade(.25f, .25f))
                .Append(_spriteRenderer.DOFade(1f, .25f))
                .Pause()
                .SetAutoKill(false);
        }

        private void PlayDamageAnimation(Image target)
        {
            var targetTransform = target.transform;
            DOTween.Sequence()
                .Append(targetTransform.DOLocalMoveY(-50f, .4f)
                    .SetEase(Ease.InCirc))
                .Join(targetTransform.DOLocalRotate(new Vector3(0f, 0f, -15f), .32f)
                    .SetEase(Ease.InSine))
                .Join(target.DOFade(0f, .32f)
                    .SetEase(Ease.OutSine))
                .AppendCallback(() => target.enabled = false);
        }
    }
}
