using UnityEngine;

namespace GoldenFur.ScriptableObjects.Primitives
{
    [CreateAssetMenu(fileName = "Boolean", menuName = "GoldenFur/Primitives/Boolean", order = 0)]
    public class BooleanValue : ScriptableObject
    {
        public bool value;
    }
}