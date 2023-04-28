using GoldenFur.Common;
using UnityEditor;
using UnityEngine;

namespace GoldenFur.Editor.Editor
{
    [CustomEditor(typeof(DirtableSo)), CanEditMultipleObjects]
    public class DirtableSoEditor : UnityEditor.Editor
    {

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Left"))
            {
                foreach (var t in targets)
                {
                    var scriptableObjectTarget = (DirtableSo) t;
                    EditorUtility.SetDirty(scriptableObjectTarget);
                }
            }

            DrawDefaultInspector ();
        }
    }
}