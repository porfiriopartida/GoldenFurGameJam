using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Float", menuName = "GoldenFur/Primitives/Float")]
    public class FloatValue : ScriptableObject
    {
        public float value;
    }
}