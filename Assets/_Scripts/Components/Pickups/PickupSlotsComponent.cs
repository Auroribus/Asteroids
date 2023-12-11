using _Scripts.Components.Shared;
using UnityEngine;

namespace _Scripts.Components.Pickups
{
    public class PickupSlotsComponent : TickComponent
    {
        [SerializeField] private PickupSlotComponent[] _slots;

        public void TryCollect(PickupComponent pickup)
        {
            foreach (var slot in _slots)
            {
                if (slot.HasPickup)
                {
                    continue;
                }

                if (slot.TryCollect(pickup))
                {
                    return;
                }
            }
        }
        
        public override void Tick()
        {
            foreach (var slot in _slots)
            {
                slot.Tick();
            }
        }
    }
}