using UnityEngine;

namespace GoldenFur.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Database", menuName = "GoldenFur/EntitiesDB", order = 0)]
    public class FloorFragmentsDatabase : ScriptableObject
    {
        // public float fragmentLength; //TODO: if this is an array of dynamic lengths?
        public GameObject[] floorFragmentPrefabs;
    }
}