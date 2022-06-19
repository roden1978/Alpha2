using System.Collections;
using GameObjectsScripts;
using PlayerScripts;
using UnityEngine;

namespace EnemyScripts
{
    public class Aggro : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private ShootPoint _shootPoint;
        [SerializeField] private Bullet _bullet;
        private float _cooldown;
        
        private Coroutine _shooting;

        private void OnEnable()
        {
            _triggerObserver.TriggerEnter += OnAggroTriggerEnter;
            _triggerObserver.TriggerExit += OnAggroTriggerExit;
        }

        private void OnDisable()
        {
            _triggerObserver.TriggerEnter -= OnAggroTriggerEnter;
            _triggerObserver.TriggerExit -= OnAggroTriggerExit;
        }

        public void Construct(float cooldown)
        {
            _cooldown = cooldown;
        }

        private void OnAggroTriggerEnter(Collider2D obj) => 
            ShootOn(obj);

        private void OnAggroTriggerExit(Collider2D obj) => 
            ShootOff();

        private void ShootOn(Component target)
        {
            _shooting = StartCoroutine(Shooting(target.transform));
        }

        private void ShootOff()
        {
            StopCoroutine(_shooting);
        }

        private IEnumerator Shooting(Transform target)
        {
            while(gameObject.activeInHierarchy)
            {
                Vector3 position = _shootPoint.transform.position;
                Vector2 lookDirection = LookDirection();
                Vector2 targetSide = TargetSide(target);
                if(targetSide != lookDirection)
                    Flip();
                Bullet bullet = Instantiate(_bullet, position, Quaternion.identity);
                Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
                Vector2 direction = DirectionToTarget(target.position  + Vector3.up, position);
                bullet.SetRotateDirection(direction);
                bulletRigidbody.AddForce(direction * bullet.Speed, ForceMode2D.Impulse);
                yield return new WaitForSeconds(_cooldown);
            }
        }
        private Vector2 DirectionToTarget(Vector3 target, Vector3 position) => 
            (target - position).normalized;

        private void Flip() => 
            transform.Rotate(0f, 180f, 0f);

        private Vector2 LookDirection()
        {
            float shootPointPosition = _shootPoint.transform.position.x;
            float viewTransformPosition = transform.position.x;
            return shootPointPosition > viewTransformPosition ? Vector2.right : Vector2.left;
        }

        private Vector2 TargetSide(Transform target) => 
            target.position.x > transform.position.x ? Vector2.right : Vector2.left;
        
       
    }
}