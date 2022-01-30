using PlayerScripts;
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
        
        private void Update()
        {
            FloatingMove();
        }

        private void FloatingMove()
        {
            if(_speed > 0)
            {
                Vector3 position = transform.position;
                float newY = _startY + _height * Mathf.Sin(Time.time * _speed);
                transform.position = new Vector3(position.x, newY, 0);
            }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out InteractableObjectsCollector collector))
                collector.Collect(this);
        }
    }
}