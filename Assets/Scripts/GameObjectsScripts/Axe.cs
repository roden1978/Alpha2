using System.Collections;
using PlayerScripts;
using Services.Pools;
using UnityEngine;
using DG.Tweening;
namespace GameObjectsScripts
{
    public class Axe : PooledObject, IDamaging
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _speed;
        [SerializeField] private int _lifeTime;
        
        public float Speed => _speed;
        public int Damage => _damage;

        private float _rotateDirection;
        private Coroutine _coroutine;

        private void OnEnable()
        {
            Rotation();
            _coroutine = StartCoroutine(LifeTime(_lifeTime));
        }

        private void OnDisable()
        {
            if (_coroutine != null) 
                StopCoroutine(_coroutine);
        }

        public override void ReturnToPool()
        {
            Hide();
        }

        private IEnumerator LifeTime(int lifeTime)
        {
            yield return new WaitForSeconds(lifeTime);
            ReturnToPool();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out IHealth health))
            {
                health.TakeDamage(Damage);
            }
            ReturnToPool();
        }

        public void SetRotateDirection(Vector2 direction)
        {
            _rotateDirection = direction.x;
        }

        private void Rotation()
        {
            transform.DOLocalRotate(new Vector3(0, 0, -360 * _rotateDirection), .3f, RotateMode.FastBeyond360)
                .SetRelative(true)
                .SetLoops(-1)
                .SetEase(Ease.Linear);
        }
    }
}
