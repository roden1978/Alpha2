using System;
using PlayerScripts;
using UnityEngine;
namespace GameObjectsScripts
{
    public abstract class PickableObject : InteractableObject
    {
        public Action PickUp;
        public virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out InteractableObjectsCollector collector))
            {
                collector.Collect(this);
                PickUp?.Invoke();
            }
        }
    }
}