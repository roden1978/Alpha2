using GameScripts;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(EnemySpawnPoint))]
    public class EnemySpawnEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(EnemySpawnPoint enemySpawnPoint, GizmoType gizmo)
        {
            float radius = enemySpawnPoint.CalculateRadius(); 
            if (radius > 0)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(enemySpawnPoint.transform.position, radius);
               
            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(enemySpawnPoint.transform.position, Vector3.one.magnitude); 
            }
            
            //enemySpawnPoint = (EnemySpawnPoint) target;
            enemySpawnPoint.UpdateTransform();
        }

        /*private void OnValidate()
        {
            EnemySpawnPoint 
        }*/
    }
}