using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.Components.Shared
{
    public class RandomMoveComponent : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Vector2 _speedRange;

        public void IncreaseVelocity(float factor)
        {
            _rigidbody2D.velocity *= factor;
        }
        
        private void OnEnable()
        {
            var speed = Random.Range(_speedRange.x, _speedRange.y);
            var direction = new Vector2(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f));
            _rigidbody2D.velocity = direction * speed;
        }

        private void OnDisable()
        {
            _rigidbody2D.velocity = Vector2.zero;
        }
    }
}