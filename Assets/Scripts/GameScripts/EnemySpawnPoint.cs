using UnityEngine;

namespace GameScripts
{
    [ExecuteInEditMode]
    public class EnemySpawnPoint : MonoBehaviour
    {
         public GameObject _prefab;
         public Vector3 _position;
         public Quaternion _rotation;
         public string _name;

        private void Start()
        {
            var pointTransform = transform;
            _position = pointTransform.position;
            _rotation = pointTransform.rotation;
            _name = gameObject.name;
        }

        private void Update()
        {
                _position = transform.position;
                _rotation = transform.rotation;
                _name = gameObject.name;
        }
        private void OnDrawGizmos()
        {
            if (_prefab != null)
            {
                var prefabSpriteRenderer = _prefab.GetComponentInChildren<SpriteRenderer>();
                var radius = prefabSpriteRenderer.bounds.size.y / 2;
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(transform.position, radius);
               
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
