using Infrastructure.GameStates;
using Infrastructure.Services;
using PlayerScripts;
using Services.SaveLoad;
using UnityEngine;

namespace Infrastructure
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Portal : MonoBehaviour
    {
        [SerializeField] private string portalTo;
        [SerializeField] private BoxCollider2D _boxCollider;
        private IGamesStateMachine _stateMachine;
        private ISaveLoadService _saveLoadService;

        private void Awake()
        {
            _stateMachine = ServiceLocator.Container.Single<IGamesStateMachine>();
            _saveLoadService = ServiceLocator.Container.Single<ISaveLoadService>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player player))
                Transit();
        }

        private void Transit()
        {
            _saveLoadService.SaveProgress();
            _stateMachine.Enter<LoadLevelState, string>(portalTo);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color32(170, 150, 0, 130);
            Gizmos.DrawCube(transform.position, _boxCollider.size);
        }
    }
}