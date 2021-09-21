using System;
using UnityEngine;

namespace GameScripts
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(SpriteRenderer))]
    public class EnemySpawnPoint : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        private float _radius;
        private SpriteRenderer _spawnPointSpriteRenderer;

        private void Start()
        {
            _spawnPointSpriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnDrawGizmos()
        {
            if (_prefab != null)
            {
                var prefabSpriteRenderer = _prefab.GetComponentInChildren<SpriteRenderer>();
                _radius = prefabSpriteRenderer.bounds.size.y / 2;
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(transform.position, _radius);
                DrawSprite(prefabSpriteRenderer.sprite);
            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, Vector3.one.magnitude); 
            }
        }

        private void DrawSprite(Sprite sprite)
        {
            _spawnPointSpriteRenderer.sprite = sprite;
        }
        public GameObject Prefab {get => _prefab; set => _prefab = value;}
    }
}
