using GameObjectsScripts;
using Services.Pools;
using UnityEngine;

namespace PlayerScripts
{
    public class Throw : MonoBehaviour
    {
        [SerializeField] private PoolService _poolService;
        [SerializeField] private Axe _pooledObject;
        [SerializeField] private ShootPoint _shootPoint;
        [SerializeField] [Range(5f, 10f)] private float _radarDistance;
        [SerializeField] [Range(1, 10)] private int _deltaDegree;
        [SerializeField] [Range(45, 90)] private int _startDegree;
        private SpriteRenderer _spriteRenderer;
        private Radar _radar;
        private float _lastDirection = Vector2.right.x;
        private Vector2 _target;

        private void Start()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _radar = new Radar(_radarDistance, _deltaDegree, _startDegree);
        }
        private void Update()
        {
            Vector2 rayDirection = Direction(_shootPoint.transform);
            if (rayDirection.x != 0) _lastDirection = rayDirection.x;

            _target = _radar.Target(_lastDirection, _shootPoint.transform.position);
            //remove before production
            if(_target != Vector2.zero)
            {
                Vector3 position = _shootPoint.transform.position;
                Debug.DrawRay(position, DirectionToTarget(_target, position) * _radarDistance, Color.red);
            }
            //remove before production
        }

        public void ThrowWeapon()
        {
            PooledObject weapon = _poolService.GetPooledObject(_pooledObject.GetType());
            if (weapon.TryGetComponent(out Rigidbody2D weaponRigidbody))
            {
                Vector3 position = _shootPoint.transform.position;
                weapon.transform.position = position;
                Vector2 direction = _target != Vector2.zero ? 
                    DirectionToTarget(_target, position) : 
                    Direction(_shootPoint.transform);
                
                weaponRigidbody.AddForce(direction * _pooledObject.Speed, ForceMode2D.Impulse);
            }                
        }

        private Vector2 Direction(Transform shootPoint)
        {
            Vector3 center = _spriteRenderer.bounds.center;
            return (center.x - shootPoint.position.x) > 0 ? Vector2.left : Vector2.right;
        }

        private Vector2 DirectionToTarget(Vector3 target, Vector3 position)
        {
            return (target - position).normalized;
        }
    }
}
