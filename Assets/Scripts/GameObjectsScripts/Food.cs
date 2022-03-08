using UnityEngine;

namespace GameObjectsScripts
{
    public class Food : PickableObject
    {
        [SerializeField] private int _health;

        private void Start()
        {
            Value = _health;
        }
    }
}