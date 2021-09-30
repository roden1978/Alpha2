using System;
using PlayerScripts;
using UnityEngine;

namespace GameObjectsScripts
{
    public class Crystal : PickableObject, IPickable
    {
        [SerializeField] private float _height;
        [SerializeField] private float _speed;
        private float _startY;

        public void PickUp()
        {
            Debug.Log("I was picked up!");
        }

        private void Start()
        {
            _startY = transform.position.y;
        }

        private void Update()
        {
            var position = transform.position;
            var newY = _startY + _height * Mathf.Sin(Time.time * _speed);
            transform.position = new Vector3(position.x, newY, 0);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (!collider.gameObject.TryGetComponent(out Player player)) return;
            Destroy(gameObject);
            Debug.Log("Collect");
        }
    }
}