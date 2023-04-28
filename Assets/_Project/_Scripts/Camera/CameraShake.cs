using System.Collections;
using GoldenFur.Event;
using GoldenFur.ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GoldenFur.Camera
{
    public class CameraShake : CharacterEventListener
    {
        public ShakeParameters shakeParameters;
        private void Start()
        {
            CharacterEventManager.Instance.Add(this);
        }

        // private void OnDestroy()
        // {
        //     Debug.Log("Disabling Camera.");
        //     CharacterEventManager.Instance.Remove(this);
        // }

 
        private IEnumerator Shake()
        {
            var startTime = Time.realtimeSinceStartup;
            var originalPosition = transform.position;
            var position = transform.position;
            
        
            while (Time.realtimeSinceStartup-startTime < shakeParameters.duration)
            {
                var x = Random.Range(shakeParameters.shakeRange.x, shakeParameters.shakeRange.y) * shakeParameters.magnitude;
                var y = Random.Range(shakeParameters.shakeRange.x, shakeParameters.shakeRange.y) * shakeParameters.magnitude;
                
                position = new Vector3(position.x + x, position.y + y, originalPosition.z);
                transform.position = position;
                yield return 0;
            }
            transform.position = originalPosition;
        }
        
        public override void OnPlayerHit()
        {
            TriggerShake();
        }

        public void TriggerShake()
        {
            StartCoroutine(Shake());
        }
    }
}