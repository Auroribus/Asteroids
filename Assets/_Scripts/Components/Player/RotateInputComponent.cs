using _Scripts.Components.Shared;
using UnityEngine;

namespace _Scripts.Components.Player
{
    public class RotateInputComponent : TickComponent
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _rotationSpeed;
        
        private float _rotation;

        public override void Tick()
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                _rotation = _rotationSpeed;
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                _rotation = -_rotationSpeed;
            }
            else
            {
                _rotation = 0f;
            }

            _rigidbody2D.rotation += _rotation * Time.deltaTime;
        }
    }
}