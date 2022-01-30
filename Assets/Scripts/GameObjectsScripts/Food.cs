using UnityEngine;

namespace GameObjectsScripts
{
    public class Food : PickableObject
    {
        [SerializeField] private int _health;

        protected override void Start()
        {
            base.Start();
            Value = _health;
        }
    }
}