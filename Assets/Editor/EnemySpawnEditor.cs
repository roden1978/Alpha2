using System;
using Infrastructure;
using StaticData;
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
            Gizmos.color = Color.red;
            Vector3 position = enemySpawner.transform.position;
            Vector3 spriteSize = spriteRenderer.sprite.bounds.size;
            Gizmos.DrawWireCube(position, spriteSize);
            Vector2 rectPosition = new Vector2(position.x - 1, position.y + 1);
            Vector2 rectSize = new Vector2(spriteSize.x, spriteSize.y * -1);
            Gizmos.DrawGUITexture(new Rect(rectPosition, rectSize), spriteRenderer.sprite.texture);
            Vector3 labelPosition = new Vector3(position.x - spriteSize.x / 2, position.y + spriteSize.y / 2 + .5f, 0);
            string text =
                $"Enemy type: {Enum.GetName(typeof(EnemyTypeId), enemySpawner.EnemyStaticData.EnemyTypeId)?.ToUpper()}";
            Handles.Label(labelPosition, text);
        }
    }
}