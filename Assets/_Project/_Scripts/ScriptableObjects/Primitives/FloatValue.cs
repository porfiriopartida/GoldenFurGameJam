using UnityEngine;

namespace GoldenFur.ScriptableObjects.Primitives
{
    [CreateAssetMenu(fileName = "Float", menuName = "GoldenFur/Primitives/Float")]
    public class FloatValue : ScriptableObject
    {
        public float value;
    }
}