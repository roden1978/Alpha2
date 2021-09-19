using UnityEngine;

namespace GameScripts
{
    [ExecuteInEditMode]
    public class EnemySpawnPoint : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        private float _radius;
        private void OnDrawGizmos()
        {
            if (_prefab != null)
            {
                var spriteRenderer = _prefab.GetComponentInChildren<SpriteRenderer>();
                _radius = spriteRenderer.bounds.size.y / 2;
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(transform.position, _radius);
            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, Vector3.one.magnitude); 
            }
        }
        public GameObject Prefab {get => _prefab; set => _prefab = value;}
    }
}
