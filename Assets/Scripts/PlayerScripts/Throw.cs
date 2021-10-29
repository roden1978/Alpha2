using System.Collections;
using GameObjectsScripts;
using UnityEngine;

namespace PlayerScripts
{
    public class Throw : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;
        private SpriteRenderer _spriteRenderer;
        private Vector3 _center;
        private void Start()
        {
            _spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            _center = _spriteRenderer.bounds.center;
        }

        public void ThrowAxe(Transform shootPoint)
        {
            var weapon = Instantiate(_weapon, shootPoint.position, Quaternion.identity);
            var weaponRigidbody = weapon.GetComponent<Rigidbody2D>();
            var direction = Direction(shootPoint);
            weaponRigidbody.AddForce(direction * _weapon.Speed, ForceMode2D.Impulse);
            StartCoroutine(AxeDie(weapon));
        }

        private Vector2 Direction(Transform shootPoint)
        {
            return (_center.x - shootPoint.position.x) > 0 ? Vector2.left : Vector2.right;
        }

        private IEnumerator AxeDie(Component weapon)
        {
            yield return new WaitForSeconds(2);
            Destroy(weapon.gameObject);
        }

        public void ChangeWeapon(Weapon newWeapon)
        {
            _weapon = newWeapon;
        }
    }
}
