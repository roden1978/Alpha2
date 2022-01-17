using System.Collections;
using Common;
using Services.Pools;
using UnityEngine;

namespace GameObjectsScripts
{
    public class Axe : PooledObject, IDamaging
    {
        [SerializeField] private int _damage = 5;
        [SerializeField] private float _speed = 10;
        [SerializeField] private int _lifeTime = 2;

        public float Speed => _speed;
        public int LifeTime => _lifeTime;
        public int Damage => _damage;

        public override void ReturnToPool()
        {
            Hide();
        }
    }
}
