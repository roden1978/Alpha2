using Infrastructure.GameStates;
using Infrastructure.Services;
using PlayerScripts;
using UnityEngine;

namespace Infrastructure
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Portal : MonoBehaviour
    {
        [SerializeField] private string portalTo;
        private BoxCollider2D _boxCollider;
        private IGamesStateMachine _stateMachine;

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider2D>();
            _stateMachine = ServiceLocator.Container.Single<IGamesStateMachine>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player player))
                Transit();
        }

        private void Transit()
        {
            _stateMachine.Enter<LoadLevelState>(portalTo);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color32(170, 150, 0, 130);
            Gizmos.DrawCube(transform.position, _boxCollider.size);
        }
    }
}