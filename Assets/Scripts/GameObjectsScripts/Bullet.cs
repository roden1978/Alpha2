using System.Collections;
using UnityEngine;

namespace GameObjectsScripts
{
    public class Bullet : PickableObject
    {
        [SerializeField] private int _damage;
        [SerializeField] private int _lifeTime;
        [SerializeField] private float _speed;
        private Coroutine _coroutine;
        public float Speed => _speed;
        private void Start()
        {
            Value = _damage;
            _coroutine = StartCoroutine(LifeTimeControlling());
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
    }
}