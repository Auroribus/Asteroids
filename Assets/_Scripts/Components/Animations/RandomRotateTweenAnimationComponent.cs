using DG.Tweening;
using UnityEngine;

namespace _Scripts.Components.Animations
{
    public class RandomRotateTweenAnimationComponent : MonoBehaviour
    {        
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _rotation;
        [SerializeField] private Vector2 _speedRange;
        [SerializeField] private LoopType _loopType;
        
        private void Start()
        {
            var speed = Random.Range(_speedRange.x, _speedRange.y);
            var direction = Random.Range(0, 2) == 0 ? 1f : -1f;
            
            _target.DOLocalRotate(_rotation * direction, speed, RotateMode.LocalAxisAdd)
                .SetSpeedBased(true)
                .SetEase(Ease.Linear)
                .SetLoops(-1, _loopType)
                .SetLink(gameObject, LinkBehaviour.PauseOnDisableRestartOnEnable);
        }
    }
}