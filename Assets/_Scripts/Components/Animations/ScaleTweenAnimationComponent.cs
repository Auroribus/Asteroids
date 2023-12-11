using DG.Tweening;
using UnityEngine;

namespace _Scripts.Components.Animations
{
    public class ScaleTweenAnimationComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _scaleValue;
        [SerializeField] private float _scaleDuration;
        [SerializeField] private Ease _scaleEase;
        
        private void Start()
        {
            _target.DOScale(_scaleValue, _scaleDuration)
                .SetEase(_scaleEase)
                .SetLink(gameObject, LinkBehaviour.PauseOnDisableRestartOnEnable);
        }
    }
}