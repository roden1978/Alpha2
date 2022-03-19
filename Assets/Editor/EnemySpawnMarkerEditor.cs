using System;
using System.Linq;
using GameObjectsScripts;
using Infrastructure.EnemySpawners;
using StaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Editor
{
    [CustomEditor(typeof(SpawnMarker))]
    public class EnemySpawnMarkerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(SpawnMarker spawnMarker, GizmoType gizmo)
        {
            SpriteRenderer spriteRenderer = spawnMarker.EnemyStaticData.Prefab.GetComponentInChildren<SpriteRenderer>();
            Gizmos.color = Color.red;
            Vector3 position = spawnMarker.transform.position;
            Vector3 spriteSize = spriteRenderer.sprite.bounds.size;
            Gizmos.DrawWireCube(position, spriteSize);
            Vector2 rectPosition = new Vector2(position.x - 1, position.y + 1);
            Vector2 rectSize = new Vector2(spriteSize.x, spriteSize.y * -1);
            Gizmos.DrawGUITexture(new Rect(rectPosition, rectSize), spriteRenderer.sprite.texture);
            Vector3 labelPosition = new Vector3(position.x - spriteSize.x / 2, position.y + spriteSize.y / 2 + .5f, 0);
            string text =
                $"{Enum.GetName(typeof(EnemyTypeId), spawnMarker.EnemyStaticData.EnemyTypeId)?.ToUpper()}";
            Handles.Label(labelPosition, text);
        }
    }
    
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            LevelStaticData levelData = (LevelStaticData)target;
            if (GUILayout.Button("Collect"))
            {
                levelData.EnemySpawners = FindObjectsOfType<SpawnMarker>()
                    .Select(x =>
                        new EnemySpawnerData(x.GetComponent<UniqueId>().Id, x.EnemyStaticData.EnemyTypeId, x.transform.position))
                    .ToList();
                levelData.LevelKey = SceneManager.GetActiveScene().name;
            }

            if (GUILayout.Button("Clear"))
            {
                levelData.EnemySpawners.Clear();
            }
            EditorUtility.SetDirty(target);
        }
    }
}