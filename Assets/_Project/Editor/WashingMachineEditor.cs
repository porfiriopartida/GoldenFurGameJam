using GoldenFur.ScriptableObjects;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

namespace GoldenFur.Editor.Editor
{
    
    
    [CustomEditor(typeof(WashingMachine)), CanEditMultipleObjects]
    public class WashingMachineEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("SET_DIRTY"))
            {
                var go = (WashingMachine) target;
                EditorUtility.SetDirty(go);

                foreach (var piece in go.laundry)
                {
                    EditorUtility.SetDirty(piece);
                }
            }
        }
    }
}