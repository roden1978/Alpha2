using UnityEngine;

namespace GameObjectsScripts
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private int _lifeTime;

        public float Speed => _speed;
        public int LifeTime => _lifeTime;
    }
}
