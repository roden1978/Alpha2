using GameObjectsScripts;
using Services.Pools;
using UnityEngine;

namespace PlayerScripts
{
    public class Throw : MonoBehaviour
    {
        [SerializeField] private Axe _pooledObject;
        [SerializeField] private ShootPoint _shootPoint;
        [SerializeField] [Range(5f, 10f)] private float _radarDistance;
        [SerializeField] [Range(.1f, 1f)] private float _deltaDegree;
        [SerializeField] [Range(45, 90)] private int _startDegree;
        [SerializeField] private AudioSource _throwSoundFx;

        private PoolService _poolService;
        private Crosshair _crosshair;
        private SpriteRenderer _spriteRenderer;
        private Radar _radar;
        private float _lastDirection = Vector2.right.x;
        private Vector2 _target;
        private Vector2 _crosshairPrevPosition;

        public void Construct(PoolService poolService, Crosshair crosshair)
        {
            _poolService = poolService;
            _crosshair = crosshair;
        }

        private void Start()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _radar = new Radar(_radarDistance, _deltaDegree, _startDegree);
        }
        private void Update()
        {
            _target = CalculateTarget();
            DrawCrosshair(_target);
        }

        private Vector2 CalculateTarget()
        {
            _lastDirection = Direction(_shootPoint.transform).x;
            return _radar.Target(_lastDirection, _shootPoint.transform.position);
        }

        public void ThrowWeapon()
        {
            Axe weapon = (Axe)_poolService.GetPooledObject(_pooledObject.GetType());
            Throwing(weapon);
            PlayThrowWeaponSoundFx();
        }

        private void Throwing(Axe weapon)
        {
            if (weapon.TryGetComponent(out Rigidbody2D weaponRigidbody))
            {
                Vector3 position = _shootPoint.transform.position;
                weapon.transform.position = position;
                Vector2 direction = _target != Vector2.zero
                    ? DirectionToTarget(_target, position)
                    : Direction(_shootPoint.transform);
                weapon.SetRotateDirection(direction);
                weapon.Show();
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

        private void DrawCrosshair(Vector2 position)
        {
            if(_crosshairPrevPosition != position && position != Vector2.zero)
            {
                _crosshair.transform.position = position;
                _crosshair.Show();
            }
            
            if(_crosshair.gameObject.activeInHierarchy && position == Vector2.zero)
                _crosshair.Hide();

            _crosshairPrevPosition = position;
        }

        private void PlayThrowWeaponSoundFx()
        {
            _throwSoundFx.Play();
        }
    }
}
