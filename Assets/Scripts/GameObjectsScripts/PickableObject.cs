using UnityEngine;

namespace GameObjectsScripts
{
    public abstract class PickableObject: MonoBehaviour
    {
        [SerializeField] private int _value;
        [SerializeField] private float _height;
        [SerializeField] private float _speed;
        private float _startY;
        public int Value => _value;

        private void Start()
        {
            _startY = transform.position.y;
        }
        
        protected void FloatingMove()
        {
            var position = transform.position;
            var newY = _startY + _height * Mathf.Sin(Time.time * _speed);
            transform.position = new Vector3(position.x, newY, 0);
        }

    }
}