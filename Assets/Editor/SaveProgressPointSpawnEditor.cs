using System;
using Infrastructure.SavePointSpawners;
using StaticData;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(SaveProgressPointMarker))]
    public class SaveProgressPointSpawnEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(SaveProgressPointMarker saveProgressPointMarker, GizmoType gizmo)
        {
            Gizmos.color = new Color32(0, 20, 170, 130);
            Vector3 position = saveProgressPointMarker.transform.position;
            SaveProgressPointStaticData staticData = saveProgressPointMarker.SaveProgressPointStaticData;
            float colliderWidth = staticData.ColliderWidth;
            float colliderHeight = staticData.ColliderHeight;
            Gizmos.DrawCube(position, new Vector3(colliderWidth, colliderHeight, 0f));
            Vector3 labelPosition = new Vector3(position.x - colliderWidth / 2, 
                position.y + colliderHeight / 2 + .5f, 0);
            string text =
                $"{Enum.GetName(typeof(SaveProgressPointTypeId), staticData.SaveProgressPointTypeId)?.ToUpper()}";
            Handles.Label(labelPosition, text);
        }

       
    }
}