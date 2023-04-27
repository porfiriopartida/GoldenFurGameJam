using GoldenFur.ScriptableObjects;
using UnityEngine;

namespace GoldenFur.Collectible
{
    public class Spinning : MonoBehaviour
    {
        public FloatValue speed;
        public Transform meshTransform;

        void Update()
        {
            meshTransform.Rotate(Vector3.up, speed.value * Time.deltaTime);
        }
    }
}
