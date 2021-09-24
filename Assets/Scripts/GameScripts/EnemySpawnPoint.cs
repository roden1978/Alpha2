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
                var prefabSpriteRenderer = _prefab.GetComponentInChildren<SpriteRenderer>();
                _radius = prefabSpriteRenderer.bounds.size.y / 2;
                Gizmos.color = Color.blue;
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
