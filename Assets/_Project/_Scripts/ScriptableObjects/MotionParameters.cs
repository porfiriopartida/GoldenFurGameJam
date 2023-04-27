using UnityEngine;

namespace GoldenFur.ScriptableObjects
{
    [CreateAssetMenu(fileName = "MotionParameters", menuName = "GoldenFur/Parameters/MotionParameters")]
    public partial class MotionParameters : ScriptableObject
    {
        [Header("Movement")]
        public float runSpeed = 2f;
        public float laneSwapSpeed = 2f;
        public float laneSwapThreshold = 2f;
        // public float sprintSpeed = 6f;
        // public float ledgeMoveSpeed = 1f;
        [Space(3)]
        
        [Header("Jumping values")]
        public float jumpSpeed = 6f;
        // public float airJumpSpeed = 6f;
        public bool jumpingRequiresGround;
        //TODO: migrate to height based jumps system
        [Space(1)]
        
        //Air behavior parameters
        // public bool airMovementRequiresGround;
        // public int maxAirJumps;
        // public bool canAirJump;
        [Space(1)]
        
        [Header("Gravity values")]
        public float goingUpGravity = -12f;
        // public float lowJumpGravity = -30f;
        // public float lowJumpSprintingGravity = -20f;
        public float apexGravity = -10f;
        public float peakReachedFallingGravity = -15f;
        public float heightPeakThreshold = .1f;
        [Space(2)]
        
        [Header("Player correction factors")] 
        public float coyoteFactor;
        public float bufferedJump;
        [Header("Sliding params")] public float slideDuration; // maybe animation based? tweakable.
        [Header("Collition values")] public float frontCollisionDetect;
        public CollisionParameters defaultCollisionParameters;
        public CollisionParameters slidingCollisionParameters;
    }
}