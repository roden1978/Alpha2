using UnityEngine;

namespace GameObjectsScripts
{
    public class Fruit : PickableObject
    {
        [SerializeField] private int _scores;

        private void Start()
        {
            Value = _scores;
        }
    }
}