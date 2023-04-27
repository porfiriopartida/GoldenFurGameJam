using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(Rigidbody))]
    public class Character : MonoBehaviour
    {
        public float wide;
        public Vector3 direction;
        private Rigidbody _rigidbody;
        private Transform _characterPosition;
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _characterPosition = GetComponent<Transform>();
        }
        private void Update()
        {
            //TODO: this avoids gravity, use alternative with force? or adjust on fixedupdate.
            _rigidbody.velocity = direction;

            InputCheck();
        }

        private void InputCheck()
        {
            //TODO: Use horizontal/vertical or hardcoded keys?.
            var aPressed = Input.GetKeyDown(KeyCode.A);
            var dPressed = Input.GetKeyDown(KeyCode.D);
            //var wPressed = Input.GetKeyDown(KeyCode.W); // jump trigger?
            
            if (aPressed)
            {
                Move(false);
            } else if (dPressed)
            {
                Move(true);
            }
            //if(wPressed) Jump(); //TODO: can jump during move?
        }

        public void Move(bool right)
        {
            var position = _characterPosition.position;
            if (right)
            {
                position = new Vector3(position.x + wide, position.y, position.z);
            }
            else
            {
                position = new Vector3(position.x - wide, position.y, position.z);
            }
            
            _characterPosition.position = position;
        }
    }
}
