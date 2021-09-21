using System;
using PlayerScripts;
using UnityEngine;

namespace GameScripts
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerSpawnPoint : MonoBehaviour
    {
        [SerializeField] private Player _player;
        private float _radius;
        private SpriteRenderer _spawnPointSpriteRenderer;

        private void Start()
        {
            _spawnPointSpriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnDrawGizmos()
        {
            if (_player != null)
            {
                var prefabSpriteRenderer = _player.GetComponentInChildren<SpriteRenderer>();
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

        private void OnValidate()
        {
            if(_player == null)
                Debug.LogError("Add Player to Player spawn point");
        }

        private void DrawSprite(Sprite sprite)
        {
            _spawnPointSpriteRenderer.sprite = sprite;
        }
    }
}
