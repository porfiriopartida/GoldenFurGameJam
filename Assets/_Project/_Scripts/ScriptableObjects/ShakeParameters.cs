using UnityEngine;

namespace GoldenFur.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ShakeParameters", menuName = "GoldenFur/Parameters/ShakeParameters", order = 0)]
    public class ShakeParameters : ScriptableObject
    {
        public float duration;
        public float magnitude;
        public Vector2 shakeRange;
    }
}