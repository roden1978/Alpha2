using Common;
using GameObjectsScripts;
using UnityEngine;

namespace PlayerScripts
{
    public class Throw : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;
        private WeaponPools _weaponPools;
        private SpriteRenderer _spriteRenderer;
        private Vector3 _center;

        private void Start()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _weaponPools = FindObjectOfType<WeaponPools>();
        }

        public void ThrowWeapon(Transform shootPoint)
        {
            var weapon = _weaponPools.GetPooledObject(_weapon.GetType());
            if (weapon.TryGetComponent(out Rigidbody2D weaponRigidbody))
            {
                weapon.transform.position = shootPoint.position;
                var direction = Direction(shootPoint);
                weaponRigidbody.AddForce(direction * _weapon.Speed, ForceMode2D.Impulse);
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

        public void ChangeWeapon(Weapon newWeapon)
        {
            _weapon = newWeapon;
        }
    }
}
