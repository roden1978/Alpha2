using Data;
using Infrastructure.GameStates;
using Infrastructure.Services;
using PlayerScripts;
using Services.PersistentProgress;
using UnityEngine;

namespace Infrastructure
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Portal : MonoBehaviour
    {
        [SerializeField] private string _portalTo;
        [SerializeField] private BoxCollider2D _boxCollider;
        private IGamesStateMachine _stateMachine;
        private IPersistentProgressService _persistentProgressService;

        private void Awake()
        {
            _stateMachine = ServiceLocator.Container.Single<IGamesStateMachine>();
            _persistentProgressService = ServiceLocator.Container.Single<IPersistentProgressService>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player player))
            {
                Transit();
            }
        }

        private void Transit()
        {
            UpdateProgressData();
            _stateMachine.Enter<LoadLevelState, string>(_portalTo);
        }

        private void UpdateProgressData()
        {
            _persistentProgressService.PlayerProgress.WorldData.PositionOnLevel.Position = Vector3.zero.AsVector3Data();
            _persistentProgressService.PlayerProgress.WorldData.PositionOnLevel.SceneName = _portalTo;
            _persistentProgressService.PlayerProgress.SaveProgressPointData.UsedSavePoints.Clear();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color32(170, 150, 0, 130);
            Gizmos.DrawCube(transform.position, _boxCollider.size);
        }
    }
}