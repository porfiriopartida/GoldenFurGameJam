using GoldenFur.ScriptableObjects;
using UnityEngine;

namespace GoldenFur.Common
{
    [RequireComponent(typeof(Rigidbody))]
    public class MovingTowards : MonoBehaviour
    {
        public FloatValue speed;
        public Vector3 dir;

        private void Start()
        {
            GetComponent<Rigidbody>().velocity = dir;
        }
    }
}
