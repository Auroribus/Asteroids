using UnityEngine;

namespace _Scripts.Components.Shared
{
    public class DamageCollisionComponent : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            var healthComponent = other.GetComponentInParent<HealthComponent>();
            if (healthComponent)
            {
                healthComponent.Damage(1);
            }
        }
    }
}