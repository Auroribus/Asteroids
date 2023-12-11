using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Components.Pickups
{
    public class PickupSlotUiComponent : MonoBehaviour
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private TMP_Text _keyCodeText;

        public bool HasPickup
        {
            set => _iconImage.enabled = value;
        }
        
        public Sprite IconSprite
        {
            set => _iconImage.sprite = value;
        }

        public string KeyCodeText
        {
            set => _keyCodeText.text = value;
        }
    }
}