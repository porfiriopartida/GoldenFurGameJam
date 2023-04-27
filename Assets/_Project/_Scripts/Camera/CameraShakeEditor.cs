using UnityEditor;
using UnityEngine;

namespace GoldenFur.Camera
{
    [CustomEditor(typeof(CameraShake))]
    public class CameraShakeEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            if (Application.isPlaying)
            {
                if (GUILayout.Button("Shake"))
                {
                    var cameraShake = (CameraShake) this.target;
                    cameraShake.TriggerShake();
                }
            }

            // Show default inspector property editor
            DrawDefaultInspector ();
        }
    }
}