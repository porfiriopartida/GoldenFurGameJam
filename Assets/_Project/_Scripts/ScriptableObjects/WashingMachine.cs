using UnityEditor;
using UnityEngine;

namespace GoldenFur.ScriptableObjects
{
    [CreateAssetMenu(fileName = "WashingMachine", menuName = "GoldenFur/Laundry")]
    public class WashingMachine : ScriptableObject
    {
        public ScriptableObject[] laundry;
    }
}