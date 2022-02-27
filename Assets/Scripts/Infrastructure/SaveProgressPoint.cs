using Infrastructure.Services;
using Services.SaveLoad;
using UnityEngine;

namespace Infrastructure
{
    public class SaveProgressPoint : MonoBehaviour
    {
        private SaveLoadService _saveLoadService;

        private void Awake()
        {
            _saveLoadService = ServiceLocator.Container.Single<SaveLoadService>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _saveLoadService.SaveProgress();
            Debug.Log("Progress saved");
            gameObject.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, 1f);
        }
    }
}
