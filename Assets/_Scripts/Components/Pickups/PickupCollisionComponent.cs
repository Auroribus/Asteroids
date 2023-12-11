using UnityEngine;

namespace _Scripts.Components.Pickups
{
    public class PickupCollisionComponent : MonoBehaviour
    {
        [SerializeField] private PickupSlotsComponent _slots;

        private void OnTriggerEnter2D(Collider2D other)
        {
            var pickup = other.GetComponentInParent<PickupComponent>();
            if (!pickup)
            {
                return;
            }
            
            _slots.TryCollect(pickup);
        }
    }
}