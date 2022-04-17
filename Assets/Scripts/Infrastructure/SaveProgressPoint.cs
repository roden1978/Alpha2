using System;
using Infrastructure.Services;
using PlayerScripts;
using Services.SaveLoad;
using UnityEngine;

namespace Infrastructure
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class SaveProgressPoint : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _firework;
        public Action Used;
        private BoxCollider2D _collider;
        private ISaveLoadService _saveLoadService;
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
            if(other.TryGetComponent(out Player player))
            {
                PlayFx();
                SaveProgress();
            }
        }

        private void SaveProgress()
        {
            Used?.Invoke();
            _saveLoadService.SaveProgress();
            Debug.Log("Progress saved");
            _collider.enabled = false;
        }

        private void PlayFx()
        {
            _firework.Play(true);
        }
    }
}
