using System;
using System.Collections;
using PlayerScripts;
using Services.Pools;
using UnityEngine;

namespace GameObjectsScripts
{
    public class Axe : PooledObject, IDamaging
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _speed;
        [SerializeField] private int _lifeTime;

        public float Speed => _speed;
        public int Damage => _damage;

        private void OnEnable()
        {
            StartCoroutine(LifeTime(_lifeTime));
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
    }
}
