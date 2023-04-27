using GoldenFur.Manager;
using UnityEngine;

namespace GoldenFur.Collectible
{
    public class Coin : Collectible
    {
        public AudioSource audioSource;
        public AudioClip[] clips;
        public Transform model;
        protected override void Collect()
        {
            ScoreManager.Instance.AddCoin(1);
            SoundManager.Instance.PlaySfx(audioSource, clips);
            
            //TODO: Consider pooling.
            model.gameObject.SetActive(false);
            
            //Destroy after the sound pops
            Destroy(this.gameObject, 0.5f);
        }
    }
}