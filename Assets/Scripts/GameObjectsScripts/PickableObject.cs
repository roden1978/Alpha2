using UnityEngine;
namespace GameObjectsScripts
{
    public abstract class PickableObject : InteractableObject
    {
        [SerializeField] private float _height;
        [SerializeField] protected float _speed;
        private float _startY;
        protected virtual void Start()
        {
            _startY = transform.position.y;
        }
        
        protected virtual void FloatingMove()
        {
            var position = transform.position;
            var newY = _startY + _height * Mathf.Sin(Time.time * _speed);
            transform.position = new Vector3(position.x, newY, 0);
        }
    }
}