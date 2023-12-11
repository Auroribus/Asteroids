using _Scripts.Components.Shared;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.Components.Player
{
    public class MoveInputComponent : TickComponent
    {
        [SerializeField] private SpriteRenderer _torchSprite;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _moveSpeed;
        
        private Tween _torchTween;

        public void ResetVelocity()
        {
            _rigidbody2D.velocity = Vector2.zero;
        }
        
        public override void Tick()
        {
            UpdateTorchVisuals();
            MoveForce();
        }

        private void Awake()
        {
            var color = _torchSprite.color;
            color.a = 0f;
            _torchSprite.color = color;
        }

        private void UpdateTorchVisuals()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                _torchTween?.Complete();
                _torchTween = _torchSprite.DOFade(1f, .2f);
            }
            else if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
            {
                _torchTween?.Complete();
                _torchTween = _torchSprite.DOFade(0f, .2f);
            }
        }

        private void MoveForce()
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                _rigidbody2D.AddForce(transform.up * _moveSpeed, ForceMode2D.Force);
            }
        }
    }
}