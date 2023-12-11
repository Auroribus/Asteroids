using UnityEngine;

namespace _Scripts.Components.Shared
{
    public class BoundsComponent : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _target;

        private void Awake()
        {
            if (!_camera)
            {
                _camera = Camera.main;
            }
        }

        private void Update()
        {
            var position = _target.position;
            var bottomLeft = _camera.ScreenToWorldPoint(Vector3.zero);
            var topRight = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
            var changedPosition = false;

            if (position.x < bottomLeft.x)
            {
                position.x = topRight.x;
                changedPosition = true;
            }
            else if (position.x > topRight.x)
            {
                position.x = bottomLeft.x;
                changedPosition = true;
            }

            if (position.y < bottomLeft.y)
            {
                position.y = topRight.y;
                changedPosition = true;
            }
            else if (position.y > topRight.y)
            {
                position.y = bottomLeft.y;
                changedPosition = true;
            }

            if (!changedPosition)
            {
                return;
            }
            
            _target.position = position;
        }
    }
}