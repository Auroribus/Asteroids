using UnityEngine;
using UnityEngine.Pool;

namespace _Scripts.Components.Projectiles
{
    public class SpawnProjectileComponent : MonoBehaviour
    {
        [SerializeField] private ProjectileComponent _projectilePrefab;

        private IObjectPool<ProjectileComponent> _pool;
        private IObjectPool<ProjectileComponent> Pool => _pool ??= new ObjectPool<ProjectileComponent>(
            OnCreateItem,
            OnGetItem,
            OnReleaseItem,
            OnDestroyItem,
            true,
            10,
            100);

        public void Spawn(Vector2 position, Vector2 direction, float lifetime)
        {
            var item = Pool.Get();
            item.Setup(position, direction, lifetime);
        }
        
        private ProjectileComponent OnCreateItem()
        {
            var item = Instantiate(_projectilePrefab);
            item.SetPool(Pool);
            return item;
        }

        private void OnGetItem(ProjectileComponent item)
        {
            item.gameObject.SetActive(true);
        }

        private void OnReleaseItem(ProjectileComponent item)
        {
            item.gameObject.SetActive(false);
        }

        private void OnDestroyItem(ProjectileComponent item)
        {
            Destroy(item.gameObject);
        }
    }
}