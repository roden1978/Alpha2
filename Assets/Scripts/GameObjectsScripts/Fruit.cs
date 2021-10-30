using UnityEngine;

namespace GameObjectsScripts
{
    public class Fruit : PickableObject
    {
        [SerializeField] private int _scores;

        protected override void Start()
        {
            base.Start();
            Value = _scores;
        }

        private void Update()
        {
            FloatingMove();
        }
 
    }
}