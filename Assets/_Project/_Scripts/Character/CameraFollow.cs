using System;
using UnityEngine;

namespace Character
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset;

        private void Start()
        {
            offset = this.transform.position - target.position;
        }

        private void Update()
        {
            //TODO: Try cinemachine for motion sickness?
            this.transform.position = target.position + offset;
        }
    }
}