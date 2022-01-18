using UnityEngine;

namespace GameScripts
{
    //[ExecuteInEditMode]
    public class EnemySpawnPoint : MonoBehaviour
    {
         public GameObject EnemyPrefab;
         public Vector3 _position;
         public Quaternion _rotation;
         public string _name;
         private void Start()
        {
            Transform pointTransform = transform;
            _position = pointTransform.position;
            _rotation = pointTransform.rotation;
            _name = gameObject.name;
        }

         public float CalculateRadius()
         {
             if (EnemyPrefab != null)
             {
                 SpriteRenderer spriteRenderer = EnemyPrefab.GetComponentInChildren<SpriteRenderer>();
                 return spriteRenderer.bounds.size.y / 2;
             }

             return 0f;
         }

         public void UpdateTransform()
         {
            _position = transform.position;
            _rotation = transform.rotation;
            _name = gameObject.name;
         }

         public void Spawn()
         {
             
         }
        
    }
}
