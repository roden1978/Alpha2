using Data;
using Infrastructure.Factories;
using Infrastructure.GameStates;
using Infrastructure.Services;
using Services.PersistentProgress;
using Services.SaveLoad;
using UnityEngine;

namespace Infrastructure
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Portal : MonoBehaviour
    {
        private const string PlayerLayerName = "Player";
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
            if (other.gameObject.layer == LayerMask.NameToLayer(PlayerLayerName))
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
            ServiceLocator.Container.Single<ISaveLoadService>().SaveProgress();
            _persistentProgressService.PlayerProgress.WorldData.PositionOnLevel.Position = Vector3.zero.AsVector3Data();
            _persistentProgressService.PlayerProgress.WorldData.PositionOnLevel.SceneName = _portalTo;
            _persistentProgressService.PlayerProgress.SaveProgressPointData.UsedSavePoints.Clear();
            ServiceLocator.Container.Single<IGameFactory>().ControlsPanel.gameObject.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color32(170, 150, 0, 130);
            Gizmos.DrawCube(transform.position, _boxCollider.size);
        }
    }
}