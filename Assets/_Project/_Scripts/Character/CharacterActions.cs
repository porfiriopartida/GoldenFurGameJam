using System;
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

        public Transform defaultMesh;
        public Transform slidingMesh;

        //Layers
        [SerializeField]
        private LayerMask whatIsGround;
        [SerializeField]
        public LayerMask whatIsObstacle;
        
        [Header("Empty object checkers")]
        [SerializeField]

        private Transform frontChecker;

        [Header("Empty object checkers")] 
        [SerializeField] private Transform slidingFrontChecker;
        [SerializeField] private Transform characterGroundChecker;


        [Header("Debug Fields")] [SerializeField]
        private bool isGrounded; // For debug view purposes

        [Header("Scene related objects")]
        // [SerializeField]
        // private Camera mainCamera;
        private CharacterController _characterController;

        [Header("Parameters")] [SerializeField]
        private MotionParameters motionParameters;

        [Header("Current state")]

        #region Current state

        [SerializeField]
        private DirectionAttribute directionAttribute;

        // private bool _jumpButtonIsHeld;
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

        [Header("Audio")] [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip[] jumpClips;
        [SerializeField] private AudioClip[] hitClips;

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
                UpdateCollider();
            }
        }


        private void UpdateCollider()
        {
            Debug.Log($"Updating Controller Collider to {_innerState}");
            CollisionParameters param;
            switch (PlayerState)
            {
                case PlayerState.Sliding:
                    param = motionParameters.slidingCollisionParameters;
                    slidingMesh.gameObject.SetActive(true);
                    defaultMesh.gameObject.SetActive(false);
                    break;
                default:
                    param = motionParameters.defaultCollisionParameters;
                    slidingMesh.gameObject.SetActive(false);
                    defaultMesh.gameObject.SetActive(true);
                    break;
            }

            _characterController.height = param.height;
            _characterController.radius = param.radius;
            _characterController.center = param.center;
        }

        private void Start()
        {
            slidingMesh.gameObject.SetActive(false);
            defaultMesh.gameObject.SetActive(true);
            _activeLane = Lane.Center;
            // mainCamera = Camera.main;
            directionAttribute = GetComponent<DirectionAttribute>();
            _characterController = GetComponent<CharacterController>();
            // animator = GetComponent<Animator>();
            UpdateCollider();
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
            SlideUpdate();
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
            if(!ScoreManager.Instance.active)return;
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

            directionAttribute.direction.y =
                -.5f; //TODO: This line sometimes makes the jump to feel stuck to the floor level. Removing it causes the gravity to impact on free falls (falling w/o jumps)
        }

        private void ApplyGravity()
        {
            if (_characterController.isGrounded)
            {
                //Stop falling on ground.
                directionAttribute.direction.y = 0;
                _coyoteTimeFalling = 0;
                if (this.PlayerState != PlayerState.Grounded && PlayerState != PlayerState.Sliding)
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

            if (PlayerState != PlayerState.Falling && directionAttribute.direction.y < 0 &&
                PlayerState != PlayerState.Sliding)
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
            // if (!_jumpButtonIsHeld && isJumpingAction && this.PlayerState == PlayerState.Jumping)
            // {
            //     directionAttribute.direction.y += motionParameters.lowJumpGravity * Time.deltaTime;
            // }
        }

        #region public Character API

        // public LaneWorldPosValues LaneWorldPosValues;
        private bool _canMove = true;
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
            directionAttribute.direction.x = dir.x * xMovFactor; // * Time.deltaTime;
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
                if (_activeLane == Lane.Right ||
                    Physics.Raycast(position, Vector3.right, laneLength.value, whatIsObstacle))
                {
                    return true;
                }
            }
            else
            {
                if (_activeLane == Lane.Left ||
                    Physics.Raycast(position, Vector3.left, laneLength.value, whatIsObstacle))
                {
                    return true;
                }
            }

            return false;
        }


        private void RegisterDamage()
        {
            if(!ScoreManager.Instance.active)return;
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

        public void Jump()
        {
            // _jumpButtonIsHeld = performed;
            // if (!performed)
            // {
            // return;
            // }
            var isCoyoteTiming = !isJumpingAction &&
                                 Math.Abs(Time.time - _coyoteTimeFalling) < motionParameters.coyoteFactor;
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
            SoundManager.Instance.PlaySfx(audioSource, jumpClips);
        }

        #endregion

        private void OnDrawGizmos()
        {
            var charGroundPos = characterGroundChecker.position;
            Debug.DrawRay(charGroundPos, Vector3.down * .5f, Color.yellow);
            Debug.DrawRay(charGroundPos, Vector3.left * laneLength.value, Color.green);
            Debug.DrawRay(charGroundPos, Vector3.right * laneLength.value, Color.green);

            Debug.DrawRay(frontChecker.position, Vector3.forward * motionParameters.frontCollisionDetect, Color.red);

            Debug.DrawRay(slidingFrontChecker.position, Vector3.forward * motionParameters.frontCollisionDetect,
                Color.red);
        }


        private void CheckFrontHitObstacle()
        {
            //Validate if front collision
            if (!_isSliding && Physics.Raycast(this.frontChecker.position, Vector3.forward,
                motionParameters.frontCollisionDetect, whatIsObstacle))
            {
                CharacterEventManager.Instance.OnPlayerHit();
                LevelSceneManager.Instance.PlayerHit();
                LevelSceneManager.Instance.GameOver(false);
            }
            //Validate if front collision while sliding
            else if (_isSliding && Physics.Raycast(this.slidingFrontChecker.position, Vector3.forward,
                motionParameters.frontCollisionDetect, whatIsObstacle))
            {
                LevelSceneManager.Instance.GameOver(false);
            }
        }

        #region Sliding

        private bool _isSliding = false;

        private float _nextSlideCheck;


        // public float currentTime;
        public void Slide()
        {
            if (PlayerState != PlayerState.Grounded || !_characterController.isGrounded)
                return; // can't slide if regular ground state.


            Debug.Log("Sliding");
            _isSliding = true;
            _nextSlideCheck = motionParameters.slideDuration;
            PlayerState = PlayerState.Sliding;
        }

        private void SlideUpdate()
        { 
            _nextSlideCheck -= Time.deltaTime;
            if (_nextSlideCheck < 0)
            {
                _isSliding = false;
                PlayerState = PlayerState.Grounded;
            }
            // Debug.Log($"Is Sliding : {_isSliding} - Next: {_nextSlideCheck}");
        }

        #endregion
    }
}