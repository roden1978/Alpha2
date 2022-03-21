using System;
using Infrastructure.Services;
using Services.SaveLoad;
using UnityEngine;

namespace Infrastructure
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class SaveProgressPoint : MonoBehaviour
    {
        private BoxCollider2D _collider;
        private ISaveLoadService _saveLoadService;
        public Action Used;
        

        public void Construct(float colliderWidth, float colliderHeight, bool isUsed)
        {
            _collider.size = new Vector2(colliderWidth, colliderHeight);
            _collider.enabled = !isUsed;
        }
        private void Awake()
        {
            _saveLoadService = ServiceLocator.Container.Single<ISaveLoadService>();
            _collider = GetComponent<BoxCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Used?.Invoke();
            _saveLoadService.SaveProgress();
            Debug.Log("Progress saved");
            _collider.enabled = false;
        }
        
    }
}
