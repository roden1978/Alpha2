using System;
using PlayerScripts;
using UnityEngine;
namespace GameObjectsScripts
{
    public abstract class PickableObject : InteractableObject
    {
        [field: SerializeField] public ParticleSystem CollectFX { get; private set; }
        public Action PickUp;
        public virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out InteractableObjectsCollector collector))
            {
                ShowCollectFX(other.transform);
                collector.Collect(this);
                PickUp?.Invoke();
            }
        }

        private void ShowCollectFX(Transform otherTransform)
        {
            Instantiate(CollectFX, otherTransform.position + Vector3.up , Quaternion.identity);
        }
    }
}