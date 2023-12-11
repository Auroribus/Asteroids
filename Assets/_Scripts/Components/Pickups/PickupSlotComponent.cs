using _Scripts.Components.Shared;
using Unity.Mathematics;
using UnityEngine;

namespace _Scripts.Components.Pickups
{
    public class PickupSlotComponent : TickComponent
    {
        [SerializeField] private KeyCode _keyCode;
        [SerializeField] private PickupSlotUiComponent _uiComponent;
        
        private GameObject _pickup;

        public bool HasPickup => _pickup;

        public bool TryCollect(PickupComponent pickup)
        {
            if (HasPickup)
            {
                return false;
            }

            _pickup = pickup.PrefabInstance;
            _uiComponent.HasPickup = HasPickup;
            Destroy(pickup.gameObject);
            
            return true;
        }

        public override void Tick()
        {
            if (Input.GetKeyDown(_keyCode))
            {
                TryUse();
            }
        }

        private void Awake()
        {
            _uiComponent.KeyCodeText = _keyCode.ToString();
            _uiComponent.HasPickup = HasPickup;
        }

        private bool TryUse()
        {
            if (!HasPickup)
            {
                return false;
            }

            Instantiate(_pickup, transform.position, quaternion.identity);
            _pickup = null;
            _uiComponent.HasPickup = HasPickup;
            
            return true;
        }
    }
}