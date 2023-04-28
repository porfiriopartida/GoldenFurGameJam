using GoldenFur.Common;
using UnityEngine;

namespace GoldenFur.ScriptableObjects
{
    [CreateAssetMenu(fileName = "CollitionParameters", menuName = "GoldenFur/Parameters/CollitionParameters", order = 0)]
    public class CollisionParameters : ScriptableObject
    {
        public float height;
        public float radius;
        public Vector3 center;
    }
}