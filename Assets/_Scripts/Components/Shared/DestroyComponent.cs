using UnityEngine;

namespace _Scripts.Components.Shared
{
    public class DestroyComponent : MonoBehaviour
    {
        [SerializeField] private float _delaySeconds;

        private void Awake()
        {
            Destroy(gameObject, _delaySeconds);
        }
    }
}