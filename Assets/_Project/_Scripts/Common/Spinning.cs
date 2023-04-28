using GoldenFur.ScriptableObjects;
using UnityEngine;

namespace GoldenFur.Common
{
    public class Spinning : DirtableSo
    {
        public FloatValue speed;
        public Transform meshTransform;
        public Vector3 dir = Vector3.up;

        void Update()
        {
            meshTransform.Rotate(dir, speed.value * Time.deltaTime);
        }
    }
}
