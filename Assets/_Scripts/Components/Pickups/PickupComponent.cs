using UnityEngine;

namespace _Scripts.Components.Pickups
{
    public class PickupComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _prefabInstance;
        public GameObject PrefabInstance => _prefabInstance;
    }
}