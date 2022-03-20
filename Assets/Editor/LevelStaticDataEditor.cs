using System.Linq;
using GameObjectsScripts;
using Infrastructure.EnemySpawners;
using StaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Editor
{
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
                levelData.PickableObjectSpawners = FindObjectsOfType<PickableObjectMarker>()
                    .Select(x =>
                        new PickableObjectSpawnData(x.GetComponent<UniqueId>().Id, x.PickableObjectStaticData.PickableObjectTypeId, x.transform.position))
                    .ToList();
                levelData.LevelKey = SceneManager.GetActiveScene().name;
            }

            if (GUILayout.Button("Clear level data"))
            {
                levelData.EnemySpawners.Clear();
                levelData.PickableObjectSpawners.Clear();
            }
            EditorUtility.SetDirty(target);
        }
    }
}