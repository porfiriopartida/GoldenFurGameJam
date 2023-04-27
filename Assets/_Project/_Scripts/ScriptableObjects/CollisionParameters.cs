using UnityEngine;

namespace GoldenFur.ScriptableObjects
{
    [CreateAssetMenu(fileName = "CollisionParameters", menuName = "GoldenFur/Parameters/CollisionParameter", order = 0)]
    public class CollisionParameters : ScriptableObject
    {
        //CharacterController parameters during slide/standing/jump?
        public float height;
        public float radius;
        public Vector3 center;
    }
}