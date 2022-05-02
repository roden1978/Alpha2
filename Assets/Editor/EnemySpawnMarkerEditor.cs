using System;
using Data;
using Infrastructure.EnemySpawners;
using StaticData;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(EnemySpawnMarker))]
    public class EnemySpawnMarkerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(EnemySpawnMarker enemySpawnMarker, GizmoType gizmo)
        {
            SpriteRenderer spriteRenderer = enemySpawnMarker.EnemyStaticData.Prefab.GetComponentInChildren<SpriteRenderer>();
            Gizmos.color = Color.red;
            Vector3 position = enemySpawnMarker.transform.position;
            Vector3 spriteSize = spriteRenderer.sprite.bounds.size;
            Gizmos.DrawWireCube(position, spriteSize);
            Vector2 rectPosition = new Vector2(position.x - 1, position.y + 1);
            Vector2 rectSize = new Vector2(spriteSize.x, spriteSize.y * -1);
            
            
            
            /*Gizmos.DrawGUITexture(new Rect(rectPosition, rectSize), spriteRenderer.sprite.texture);
            Vector3 labelPosition = new Vector3(position.x - spriteSize.x / 2, position.y + spriteSize.y / 2 + .5f, 0);
            string text =
                $"{Enum.GetName(typeof(EnemyTypeId), enemySpawnMarker.EnemyStaticData.EnemyTypeId)?.ToUpper()}";
            Handles.Label(labelPosition, text);*/
            
            Sprite sprite = spriteRenderer.sprite;
            Texture2D texture = sprite.texture.isReadable 
                ? sprite.TextureFromSprite()
                : sprite.texture;
            Gizmos.DrawGUITexture(new Rect(rectPosition, rectSize), texture);

            Vector3 labelPosition = new Vector3(position.x - spriteSize.x / 2, position.y + spriteSize.y / 2 + .5f, 0);
            string text =
                $"{Enum.GetName(typeof(EnemyTypeId), enemySpawnMarker.EnemyStaticData.EnemyTypeId)?.ToUpper()}";
            Handles.Label(labelPosition, text);
        }
    }
}