using UnityEngine;

namespace GoldenFur.Character
{
    [RequireComponent(typeof(Rigidbody))]
    public class Character : MonoBehaviour
    {
        public Vector3 direction;
        private Rigidbody _rigidbody;
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            
        }

        private void Update()
        {
            _rigidbody.velocity = direction;
        }
    }
}
