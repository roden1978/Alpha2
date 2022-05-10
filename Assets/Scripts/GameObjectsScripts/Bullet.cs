using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace GameObjectsScripts
{
    public class Bullet : PickableObject
    {
        [SerializeField] private int _damage;
        [SerializeField] private int _lifeTime;
        [SerializeField] private float _speed;
        [SerializeField] private bool _rotation;

        private Coroutine _coroutine;
        private float _rotateDirection;
        public float Speed => _speed;
        private void Start()
        {
            Value = _damage;
            _coroutine = StartCoroutine(LifeTimeControlling());
            if(_rotation) Rotation();
        }
        public override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
            StopCoroutine(_coroutine);
            Destroy();
        }

        private void Destroy()
        {
            Destroy(gameObject);
        }

        private IEnumerator LifeTimeControlling()
        {
            yield return new WaitForSeconds(_lifeTime);
            Destroy(gameObject);
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