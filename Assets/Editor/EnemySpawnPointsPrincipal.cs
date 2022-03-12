using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace Editor.Scripts
{
    /*public class EnemySpawnPointsPrincipal : EditorWindow
    {
        private EnemySpawnPoint[] _spawnPoints;
        private int _count;
        private bool _saveButtonEnable;
        private bool _deleteButtonEnable;
        private bool _mountButtonEnable;
        private string _resultString;
        private string _sceneName;
        private void Awake()
        {
            _resultString = string.Empty;
            _spawnPoints = Array.Empty<EnemySpawnPoint>();
            _sceneName = SceneManager.GetActiveScene().name;
            _deleteButtonEnable = true;
        }

        [MenuItem("Window/Enemy Spawn Points Principal")]
        public static void ShowWindow()
        {
            GetWindow<EnemySpawnPointsPrincipal>("Управление точками размещения врагов");
        }

        private void OnGUI()
        {
            GUILayout.Label("Find spawn points", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();
            
            if (GUILayout.Button("Find"))
            {
                FindPoints();
                ShowPointsString();
            }

            if (GUILayout.Button("Clear list"))
            {
                ClearPointsList();
            }
            GUILayout.EndHorizontal();

            GUILayout.Label("Load/Save", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Load"))
            {
                LoadData();
            }
            GUI.enabled = _saveButtonEnable;
            if (GUILayout.Button("Save"))
            {
                SaveData();
                _resultString = string.Empty;
            }
            GUI.enabled = true;
            GUILayout.EndHorizontal();

            GUILayout.Label("Mounting/Delete", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();
            GUI.enabled = _mountButtonEnable;
            if (GUILayout.Button("Mount points"))
            {
                InstantiatePoints();
                _resultString = string.Empty;
            }
            GUI.enabled = _deleteButtonEnable;
            if (GUILayout.Button("Delete points"))
            {
                
                if (FindPoints() != 0)
                {
                    for (var i = 0; i < _spawnPoints?.Length; i++)
                    {
                        DestroyImmediate(_spawnPoints[i].gameObject);
                    }

                    _spawnPoints = Array.Empty<EnemySpawnPoint>();
                }
            }
            GUILayout.EndHorizontal();
            GUI.enabled = true;
            GUILayout.Label($"Spawn point amount on scene: {_count.ToString()}", EditorStyles.boldLabel);
            GUI.enabled = false;
            GUILayout.TextArea(_resultString);
        }

        private void ClearPointsList()
        {
            _spawnPoints = Array.Empty<EnemySpawnPoint>();
            _count = 0;
            _saveButtonEnable = false;
            _deleteButtonEnable = false;
            _resultString = string.Empty;
        }

        private int FindPoints()
        {
            if (_spawnPoints.Length > 0)
            {
                _spawnPoints = Array.Empty<EnemySpawnPoint>();
                _resultString = string.Empty;
            }
            _spawnPoints = FindObjectsOfType<EnemySpawnPoint>();
            if (_spawnPoints != null)
            {
                _count = _spawnPoints.Length;
                _saveButtonEnable = true;
                _mountButtonEnable = false;
                _deleteButtonEnable = true;
            }
            return _spawnPoints?.Length ?? 0;
        }

        private void ShowPointsString()
        {
            _resultString = BuildJsonString(_spawnPoints);
        }

        private string BuildJsonString(IReadOnlyList<EnemySpawnPoint> spawnPoints)
        {
            foreach (EnemySpawnPoint point in spawnPoints)
            {
                _resultString += '"' + point.name + 
                                 '"' + 
                                 ":" + 
                                 JsonUtility.ToJson(point) + 
                                 ",";
            }

            return _resultString;
        }
        
        private void SaveData()
        {
            File.WriteAllText(Application.persistentDataPath 
                              + "/" 
                              + _sceneName 
                              + "_enemySpawnPoints.json", _resultString);
            Debug.Log("Saved " + Application.persistentDataPath);
        }
 
        private void LoadData()
        {
            string readAllText = File.ReadAllText(Application.persistentDataPath 
                                                  + "/" 
                                                  + _sceneName 
                                                  + "_enemySpawnPoints.json");
            int index = readAllText.IndexOf('"');
            
            _resultString = "{" + readAllText.Substring(index) + "}";
            if (_resultString.Length > 0) _mountButtonEnable = true;
        }

        private void InstantiatePoints()
        {
            dynamic parsed = JObject.Parse(_resultString);
            foreach (dynamic item in parsed)
            {
                string goName = item.Value._name;
                GameObject go = new GameObject(goName);
                EnemySpawnPoint enemySpawnPoint = go.AddComponent<EnemySpawnPoint>();
                Transform pointTransform = enemySpawnPoint.transform;

                float x = item.Value._position.x;
                float y = item.Value._position.y;
                float z = item.Value._position.z;
                pointTransform.position = new Vector3(x, y, z);

                float rotationX = item.Value._rotation.x;
                float rotationY = item.Value._rotation.y;
                float rotationZ = item.Value._rotation.z;
                float rotationW = item.Value._rotation.w;
                pointTransform.rotation = new Quaternion(rotationX, rotationY, rotationZ, rotationW);
                
                int id = item.Value._prefab.instanceID;
                GameObject obj = (GameObject) EditorUtility.InstanceIDToObject(id);
                enemySpawnPoint.EnemyPrefab = obj;
            }

            _mountButtonEnable = false;
            ClearPointsList();
        }
    }*/
}
