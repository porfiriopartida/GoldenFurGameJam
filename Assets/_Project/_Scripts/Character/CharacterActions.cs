﻿using System;
using GoldenFur.Common;
using GoldenFur.Event;
using GoldenFur.Manager;
using GoldenFur.ScriptableObjects;
using UnityEngine;

namespace GoldenFur.Character
{
    [RequireComponent(typeof(DirectionAttribute), typeof(CharacterController))]
    public class CharacterActions : MonoBehaviour
    {
        //Layers
        [SerializeField]
        private LayerMask whatIsGround;
        [SerializeField]
        public LayerMask whatIsObstacle;
        
        [Header("Empty object checkers")]
        [SerializeField]
        private Transform frontChecker;
        [SerializeField]
        private Transform characterGroundChecker;

        
        [Header("Debug Fields")]
        [SerializeField]
        private bool isGrounded; // For debug view purposes

        [Header("Scene related objects")]
        // [SerializeField]
        // private Camera mainCamera;
        private CharacterController _characterController;
        [Header("Parameters")]
        [SerializeField]
        private MotionParameters motionParameters;

        [Header("Current state")]
        #region Current state 
        [SerializeField]
        private DirectionAttribute directionAttribute;
        private bool _jumpButtonIsHeld;
        public bool isJumpingAction;
        private float _lastInvalidJumpTime = float.MinValue; //For buffered jump corrections
        #endregion
        private float _coyoteTimeFalling; // For coyote timing corrections

        #region Animation parameters/triggers
        // private Animator animator;
        // private static readonly int HorizontalSpeed = Animator.StringToHash("horizontalSpeed");
        // private static readonly int JumpTrigger = Animator.StringToHash("Jump");
        #endregion


        #region Lane swap related
        private Lane _activeLane;
        public FloatValue laneLength;
        private float nextXPos;
        #endregion

        #region Audio
        [Header("Audio")]
        [SerializeField]
        private AudioSource audioSource;
        [SerializeField]
        private AudioClip[] jumpClips;
        [SerializeField]
        private AudioClip[] hitClips;

        #endregion

        private PlayerState _innerState;

        private PlayerState PlayerState
        {
            get => _innerState;
            set
            {
                if (value == _innerState)
                {
                    return;
                }

                // Debug.Log($"Setting player state. from {this._innerState} to {value}");
                this._innerState = value;
            }
        }

        // private bool _canMove = true; //TODO: Remove if not used, placeholder for partial stop when hit
        private void Start()
        {
            
            _activeLane = Lane.Center;
            // mainCamera = Camera.main;
            directionAttribute = GetComponent<DirectionAttribute>();
            _characterController = GetComponent<CharacterController>();
            // animator = GetComponent<Animator>();
        }
        private void OnApplicationQuit()
        {
            Dispose();
        }
        private void Dispose()
        {
            // mainCamera = null;        
        }
        private void OnDestroy()
        {
            Dispose();
        }
        private void Update()
        {
            #region Jump
            CheckBufferedJump();
            isGrounded = _characterController.isGrounded; //debug purposes only.
            //State
            CheckState();
            #endregion
            
            //Movement
            MotionProcess();
            LaneSwappedCheck();

            //Lose Condition - hit an obstacle with the face.
            CheckFrontHitObstacle();
        }

        private void CheckBufferedJump()
        {
            if (_characterController.isGrounded)
            {
                isJumpingAction = false;
            }

            if (isGrounded || !_characterController.isGrounded) return;
            //Just arrived to the floor this frame. Validate if there was a jump intent
            if (Math.Abs(Time.time - _lastInvalidJumpTime) < motionParameters.bufferedJump)
            {
                this.PlayerState = PlayerState.StartJump;
                _lastInvalidJumpTime = float.MinValue;
            }
        }
        private void CheckState()
        {
            if (this.PlayerState == PlayerState.StartJump)
            {
                this.PlayerState = PlayerState.Jumping;
                TriggerJump();
            }
        }
        private void MotionProcess()
        {
            FixZSpeed();
            _characterController.Move(directionAttribute.direction * Time.deltaTime);
            ApplyGravity();
        }

        private void FixZSpeed()
        {
            var zMovFactor = motionParameters.runSpeed; 
            directionAttribute.direction.z = zMovFactor;
        }

        private void LateUpdate()
        {
            OnGroundedCleanUp();
        }
        private void OnGroundedCleanUp()
        {
            if (!_characterController.isGrounded) return;
            
            directionAttribute.direction.y = -.5f; //TODO: This line sometimes makes the jump to feel stuck to the floor level. Removing it causes the gravity to impact on free falls (falling w/o jumps)
        }
        private void ApplyGravity()
        {
            if (_characterController.isGrounded)
            {
                //Stop falling on ground.
                directionAttribute.direction.y = 0;
                _coyoteTimeFalling = 0;
                if (this.PlayerState != PlayerState.Grounded)
                {
                    this.PlayerState = PlayerState.Grounded;
                }
                return;
            }

            if (!isJumpingAction && _coyoteTimeFalling == 0 && this.PlayerState == PlayerState.Grounded)
            {
                //Record the moment the player started falling from a grounded state
                _coyoteTimeFalling = Time.time;
            }

            if (PlayerState != PlayerState.Falling && directionAttribute.direction.y < 0)
            {
                //Falling and unknown falling state.
                this.PlayerState = PlayerState.Falling;
            }
            if (PlayerState != PlayerState.Jumping && directionAttribute.direction.y > 0)
            {
                //Falling and unknown falling state.
                this.PlayerState = PlayerState.Jumping;
            }

            if (isJumpingAction && Math.Abs(directionAttribute.direction.y) < motionParameters.heightPeakThreshold)
            {
                //Peak gravity
                directionAttribute.direction.y += motionParameters.apexGravity * Time.deltaTime;
            }
            else
            {
                if (this.PlayerState == PlayerState.Falling)
                {
                    //Peak reached gravity falling
                    directionAttribute.direction.y += motionParameters.peakReachedFallingGravity * Time.deltaTime;
                }
                else if (this.PlayerState == PlayerState.Jumping)
                {
                    //Regular gravity to going up
                    directionAttribute.direction.y += motionParameters.goingUpGravity * Time.deltaTime;
                }
            }

            //Reduce time to reach apex.
            if (!_jumpButtonIsHeld && isJumpingAction && this.PlayerState == PlayerState.Jumping)
            {
                directionAttribute.direction.y += motionParameters.lowJumpGravity * Time.deltaTime;
            }
        }
        
        #region public Character API

        // public LaneWorldPosValues LaneWorldPosValues;
        public bool _canMove = true;
        public bool _isMoving = true;
        public bool _lastDirection = true;
        public void MoveLane(bool isRight)
        {
            if (!CanMove(isRight))
            {
                return;
            }

            var dir = isRight ? Vector3.right : Vector3.left;
            var xMovFactor = motionParameters.laneSwapSpeed;
            directionAttribute.direction.x = dir.x * xMovFactor;// * Time.deltaTime;
            var localLaneLength = this.laneLength.value;
            nextXPos = this.transform.position.x + (isRight ? localLaneLength : -localLaneLength);
            _canMove = false;
            _isMoving = true;
            _lastDirection = true;
            // Debug.Log($"Pos: {transform.position.x} -- next: {nextXPos}");
            // Debug.Log($"dir: {dir} -- isRight: {isRight}");
            // Debug.Log($"localLaneLength: {localLaneLength}");
        }
        private void LaneSwappedCheck()
        {
            var charTransform = transform;
            if (!(Math.Abs(charTransform.position.x - nextXPos) <= motionParameters.laneSwapThreshold))
            {
                // Debug.Log($"Diff: {Math.Abs(this.transform.position.x - nextXPos)}");
                return;
            }

            var curPos = charTransform.position;
            curPos.x = nextXPos;
            charTransform.position = curPos;

            //reached lane pos
            _canMove = true;
            //Stop moving
            directionAttribute.direction.x = 0;
        }

        private bool IsMovingTowardsObstacle(bool isRight)
        {
            // var position = this.transform.position;
            var position = this.characterGroundChecker.position;
            if (isRight)
            {
                //Character Transform has something to the right?
                if (_activeLane == Lane.Right || Physics.Raycast(position, Vector3.right, laneLength.value, whatIsObstacle))
                {
                    return true;
                }
            }
            else
            {
                if (_activeLane == Lane.Left || Physics.Raycast(position, Vector3.left, laneLength.value, whatIsObstacle))
                {
                    return true;
                }
            }

            return false;
        }


        private void RegisterDamage()
        {
            SoundManager.Instance.PlaySfx(audioSource, hitClips);
            LevelSceneManager.Instance.PlayerHit();
            CharacterEventManager.Instance.OnPlayerHit();
        }
        
        private bool CanMove(bool isRight)
        {
            if (!_canMove)
            {
                //Can't move during hit or lane swap
                return false;
            }

            if (IsMovingTowardsObstacle(isRight))
            {
                RegisterDamage();
                return false;
            }

            return true;
        }
        public void Jump(bool performed)
        {
            _jumpButtonIsHeld = performed;
            if (!performed)
            {
                return;
            }
            var isCoyoteTiming = !isJumpingAction && Math.Abs(Time.time - _coyoteTimeFalling) < motionParameters.coyoteFactor;
            //Coyote time calculation
            if (!_characterController.isGrounded
                && motionParameters.jumpingRequiresGround
                // && (!motionParameters.canAirJump || airJumpsLeft <= 0)
                && !isCoyoteTiming
            )
            {
                _lastInvalidJumpTime = Time.time;
                return;
            }
            this.PlayerState = PlayerState.StartJump;
        }
        private void TriggerJump()
        {
            isJumpingAction = true;
            // airJumpsLeft--;
            var jumpSpeed = motionParameters.jumpSpeed; 
                //_characterController.isGrounded ? motionParameters.jumpSpeed : motionParameters.airJumpSpeed;
            directionAttribute.direction.y = jumpSpeed;
        }

        #endregion
        
        private void OnDrawGizmos()
        {
            var charGroundPos = characterGroundChecker.position;
            Debug.DrawRay(charGroundPos, Vector3.down * .5f, Color.yellow);
            Debug.DrawRay(charGroundPos, Vector3.left * laneLength.value, Color.green);
            Debug.DrawRay(charGroundPos, Vector3.right * laneLength.value, Color.green);
            
            Debug.DrawRay(frontChecker.position, Vector3.forward * .5f, Color.red);
        }
        
        

        private void CheckFrontHitObstacle()
        {
            //Validate if front collision
            if (Physics.Raycast(this.frontChecker.position, Vector3.forward, 0.1f, whatIsObstacle))
            {
                LevelSceneManager.Instance.GameOver(false);
            }
        }
    }
    
}