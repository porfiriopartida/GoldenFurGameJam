using UnityEngine;

namespace GoldenFur.Collectible
{
    public abstract class Collectible : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Collect();
        }

        protected abstract void Collect();
    }
}