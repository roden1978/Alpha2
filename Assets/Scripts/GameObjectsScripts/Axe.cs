using System.Collections;
using Services.Pools;
using UnityEngine;

namespace GameObjectsScripts
{
    public class Axe : PooledObject, IDamaging
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _speed;
        [SerializeField] private int _lifeTime;

        private Coroutine _coroutine;
        public float Speed => _speed;
        public int Damage => _damage;

        private void Start()
        {
            _coroutine = StartCoroutine(LifeTime(_lifeTime));
        }

        public override void ReturnToPool()
        {
            Hide();
        }

        private IEnumerator LifeTime(int lifeTime)
        {
            yield return new WaitForSeconds(lifeTime);
            StopCoroutine(_coroutine);
            ReturnToPool();
        }
    }
}
