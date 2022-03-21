using System;
using Infrastructure;
using Infrastructure.EnemySpawners;
using StaticData;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(PickableObjectMarker))]
    public class PickableObjectSpawnEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(PickableObjectMarker pickableObjectMarker, GizmoType gizmo)
        {
            SpriteRenderer spriteRenderer = pickableObjectMarker.PickableObjectStaticData.Prefab.GetComponent<SpriteRenderer>();
            Gizmos.color = Color.red;
            Vector3 position = pickableObjectMarker.transform.position;
            Vector3 spriteSize = spriteRenderer.sprite.bounds.size;
            Gizmos.DrawWireCube(position, spriteSize);
            Vector3 labelPosition = new Vector3(position.x - spriteSize.x / 2, position.y + spriteSize.y / 2 + .5f, 0);
            string text =
                $"{Enum.GetName(typeof(PickableObjectTypeId), pickableObjectMarker.PickableObjectStaticData.PickableObjectTypeId)?.ToUpper()}";
            Handles.Label(labelPosition, text);
        }

       
    }
}