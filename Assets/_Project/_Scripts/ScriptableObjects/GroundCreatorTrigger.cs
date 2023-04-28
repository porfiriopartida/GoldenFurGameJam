using GoldenFur.Manager;
using UnityEngine;

namespace GoldenFur.ScriptableObjects
{
    public class GroundCreatorTrigger : MonoBehaviour
    {
        //Parent ground.
        public Transform groundReference;
        public float fragmentLength;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                SpawnNextFloor();
            }
        }

        private void SpawnNextFloor()
        {
            SpawnerManager.Instance.SpawnFragment(groundReference, fragmentLength);
        }
    }
}