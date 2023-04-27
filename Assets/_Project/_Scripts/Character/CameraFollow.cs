using ScriptableObjects;
using UnityEngine;

namespace Character
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset;
        private float _centerX;
        public FloatValue offsetFactor;
        private void Start()
        {
            var position = this.transform.position;
            offset = position - target.position;
            _centerX = position.x;
        }

        private void Update()
        {
            this.transform.position = CamPosCleanup();
        }

        private Vector3 CamPosCleanup()
        {
            var newPos = this.target.position + offset;
            //Camera ignores y offset.
            newPos.y = this.transform.position.y;
            //Camera follows x slightly
            newPos.x =  _centerX - (_centerX - newPos.x) * offsetFactor.value ;
            
            return newPos;
        }
    }
}