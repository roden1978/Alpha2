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
            SpriteRenderer spriteRenderer = enemySpawner.EnemyStaticData.Prefab.GetComponentInChildren<SpriteRenderer>();
            Gizmos.color = Color.gray;
            Vector3 position = enemySpawner.transform.position;
            Vector3 spriteSize = spriteRenderer.sprite.bounds.size;
            Gizmos.DrawWireCube(position, spriteSize);
            Vector2 rectPosition = new Vector2(position.x - 1, position.y + 1);
            Vector2 rectSize = new Vector2(spriteSize.x, spriteSize.y * -1);
            Gizmos.DrawGUITexture(new Rect(rectPosition, rectSize), spriteRenderer.sprite.texture);
        }
    }
}