using GameObjectsScripts;
using Services.Pools;
using UnityEngine;

namespace PlayerScripts
{
    public class Throw : MonoBehaviour
    {
        [SerializeField] private PoolService _poolService;
        [SerializeField] private Axe _pooledObject;
        private SpriteRenderer _spriteRenderer;
        private Vector3 _center;

        private void Start()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        public void ThrowWeapon(Transform shootPoint)
        {
            PooledObject weapon = _poolService.GetPooledObject(_pooledObject.GetType());
            if (weapon.TryGetComponent(out Rigidbody2D weaponRigidbody))
            {
                weapon.transform.position = shootPoint.position;
                Vector2 direction = Direction(shootPoint);
                weaponRigidbody.AddForce(direction * _pooledObject.Speed, ForceMode2D.Impulse);
            }                
        }

        private void FixedUpdate()
        {
            _center = _spriteRenderer.bounds.center;
        }

        private Vector2 Direction(Transform shootPoint)
        {
            return (_center.x - shootPoint.position.x) > 0 ? Vector2.left : Vector2.right;
        }
        
    }
}
