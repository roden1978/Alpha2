using Infrastructure.Services;
using Services.SaveLoad;
using UnityEngine;

namespace Infrastructure
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class SaveProgressPoint : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D _collider;
        private ISaveLoadService _saveLoadService;

        private void Awake()
        {
            _saveLoadService = ServiceLocator.Container.Single<ISaveLoadService>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _saveLoadService.SaveProgress();
            Debug.Log("Progress saved");
            gameObject.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color32(0, 20, 170, 130);
            Gizmos.DrawCube(transform.position, _collider.size);
        }
    }
}
