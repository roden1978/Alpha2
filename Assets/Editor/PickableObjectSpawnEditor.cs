using System;
using Infrastructure;
using StaticData;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(PickableObjectSpawner))]
    public class PickableObjectSpawnEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(PickableObjectSpawner pickableObjectSpawner, GizmoType gizmo)
        {
            SpriteRenderer spriteRenderer = pickableObjectSpawner.PickableObjectStaticData.Prefab.GetComponent<SpriteRenderer>();
            Gizmos.color = Color.red;
            Vector3 position = pickableObjectSpawner.transform.position;
            Vector3 spriteSize = spriteRenderer.sprite.bounds.size;
            Gizmos.DrawWireCube(position, spriteSize);
            Vector3 labelPosition = new Vector3(position.x - spriteSize.x / 2, position.y + spriteSize.y / 2 + .5f, 0);
            string text =
                $"{Enum.GetName(typeof(PickableObjectTypeId), pickableObjectSpawner.PickableObjectStaticData.PickableObjectTypeId)?.ToUpper()}";
            Handles.Label(labelPosition, text);
        }

       
    }
}