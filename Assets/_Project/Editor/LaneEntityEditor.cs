using UnityEditor;
using UnityEngine;

namespace GoldenFur.Common
{
    [CustomEditor(typeof(LaneEntity)), CanEditMultipleObjects]
    public class LaneEntityEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {

                if (GUILayout.Button("Left"))
                {
                    foreach (var t in targets)
                    {
                        var go = (LaneEntity) t;
                        var transform = go.transform;
                        var curPos = transform.position;
                        curPos.x = LaneWorldPosValues.Left;
                        transform.position = curPos;
                    }
                }
                if (GUILayout.Button("Center"))
                {
                    foreach (var t in targets)
                    {
                        var go = (LaneEntity) t;
                        var transform = go.transform;
                        var curPos = transform.position;
                        curPos.x = LaneWorldPosValues.Center;
                        transform.position = curPos;
                    }
                }
                if (GUILayout.Button("Right"))
                {
                    foreach (var t in targets)
                    {
                        var go = (LaneEntity) t;
                        var transform = go.transform;
                        var curPos = transform.position;
                        curPos.x = LaneWorldPosValues.Right;
                        transform.position = curPos;
                    }
                }
        }
    }
}