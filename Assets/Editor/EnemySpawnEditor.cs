using Infrastructure;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(EnemySpawner))]
    public class EnemySpawnEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(EnemySpawner enemySpawner, GizmoType gizmo)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(enemySpawner.transform.position, Vector3.one.magnitude);
        }
    }
}