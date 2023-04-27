using UnityEngine;

namespace GoldenFur.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Float", menuName = "GoldenFur/Primitives/Float")]
    public class FloatValue : ScriptableObject
    {
        public float value;
    }
}