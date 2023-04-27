using System;
using GoldenFur.Common;
using GoldenFur.Manager;
using GoldenFur.ScriptableObjects;
using UnityEngine;

namespace GoldenFur.Character
{
    [RequireComponent(typeof(Rigidbody))]
    public class Character : MonoBehaviour
    {
        #region Privates
        private Lane _activeLane;
        private Rigidbody _rigidbody;
        private Transform _characterPosition;
        public LayerMask whatIsObstacle;

        #endregion
        
        [Header("Move Params")]
        public FloatValue laneLength;
        public float zSpeed;
        public float jumpForce;

        [Header("What is Ground")]
        [SerializeField]
        private LayerMask whatIsGround;
        [SerializeField]
        private Transform characterGroundChecker;
        public bool isGrounded;

        [Header("Audio")]
        [SerializeField]
        private AudioSource audioSource;
        [SerializeField]
        private AudioClip[] jumpClips;

        private void Start()
        {
            _activeLane = Lane.CENTER;
            _rigidbody = GetComponent<Rigidbody>();
            _characterPosition = GetComponent<Transform>();
        }
        private void Update()
        {
            FixZSpeed();

            InputCheck();
        }

        private void FixedUpdate()
        {
            //GroundCheck
            isGrounded = Physics.Raycast(characterGroundChecker.position, Vector3.down, 2f, whatIsGround);
        }

        private void FixZSpeed()
        {
            var currentSpeed = _rigidbody.velocity;
            currentSpeed.z = zSpeed;
            _rigidbody.velocity = currentSpeed;
        }

        private void InputCheck()
        {
            //TODO: Use horizontal/vertical or hardcoded keys?.
            var aPressed = Input.GetKeyDown(KeyCode.A);
            var dPressed = Input.GetKeyDown(KeyCode.D);
            var wPressed = Input.GetKeyDown(KeyCode.W); // jump trigger?
            
            if (aPressed)
            {
                Move(false);
            } else if (dPressed)
            {
                Move(true);
            }

            if (wPressed && isGrounded) //This won't allow air jumps.
            {
                Jump();
            }
        }
        private void Jump()
        {
            SoundManager.Instance.PlaySfx(this.audioSource, jumpClips);
            _rigidbody.AddForce(Vector3.up * jumpForce);
        }

        private bool CanMove(bool isRight)
        {
            var position = this.transform.position;
            if (isRight)
            {
                //Character Transform has something to the right?
                if (_activeLane == Lane.RIGHT || Physics.Raycast(position, Vector3.right, laneLength.value, whatIsObstacle))
                {
                    return false;
                }
            }
            else
            {
                if (_activeLane == Lane.LEFT || Physics.Raycast(position, Vector3.left, laneLength.value, whatIsObstacle))
                {
                    return false;
                }
            }
            return true;
        }

        private void Move(bool isRight)
        {
            if (!CanMove(isRight))
            {
                //Hit something (lane or box?)
                RegisterDamage();
                return;
            }

            if (isRight)
            {
                _activeLane = _activeLane == Lane.CENTER ? Lane.RIGHT : Lane.CENTER;
            } else {
                _activeLane = _activeLane == Lane.CENTER ? Lane.LEFT : Lane.CENTER;
            }

            //TODO: Ideally this should do a translate instead of hardcode fix position.
            var position = _characterPosition.position;
            if (isRight)
            {
                position = new Vector3(position.x + laneLength.value, position.y, position.z);
            }
            else
            {
                position = new Vector3(position.x - laneLength.value, position.y, position.z);
            }
            
            _characterPosition.position = position;
        }

        private void RegisterDamage()
        {
            Debug.Log("Hit Something!");
        }
    }
}
