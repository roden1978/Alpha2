using GameObjectsScripts;
using Services.Pools;
using UnityEngine;

namespace EnemyScripts
{
    public class Poison : PooledObject, IDamaging
    {
        [SerializeField] private int _damage;
        [SerializeField] private int _speed;
        [SerializeField] private int _lifetime;
        public override void ReturnToPool()
        {
            Hide();
        }

        public int Damage => _damage;
    }
}
