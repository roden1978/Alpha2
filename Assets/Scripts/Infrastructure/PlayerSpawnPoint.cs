using System;
using PlayerScripts;
using UnityEngine;

namespace GameScripts
{
    [ExecuteInEditMode]
   
    public class PlayerSpawnPoint : MonoBehaviour
    {
        [SerializeField] private Player _player;
        private float _radius;
        
        private void OnDrawGizmos()
        {
            if (_player != null)
            {
                var prefabSpriteRenderer = _player.GetComponentInChildren<SpriteRenderer>();
                _radius = prefabSpriteRenderer.bounds.size.y / 2;
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(transform.position, _radius);
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
        
    }
}
