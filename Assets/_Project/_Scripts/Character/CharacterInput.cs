using GoldenFur.Manager;
using UnityEngine;

namespace GoldenFur.Character
{
    public class CharacterInput : MonoBehaviour
    {
        public CharacterActions characterActions;
        
        private void Update()
        {
            if (!LevelSceneManager.Instance.isGameRunning)
            {
                //TODO: Maybe use observer instead, many things may want to know about game over situations.
                return;
            }
            InputCheck();
        }
        private void InputCheck()
        {
            var aPressed = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) ;
            var dPressed = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
            var jumpPressed = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space);
            //Windows controller, Mac would be alt?
            var crouchPressed = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.LeftControl);
            var escPressed = Input.GetKeyDown(KeyCode.Escape);
            
            //Can only move left or right 
            if (aPressed)
            {
                characterActions.MoveLane(false);
            } else if (dPressed)
            {
                characterActions.MoveLane(true);
            }

            if (jumpPressed)
            {
                // characterActions.Jump(!crouchReleased);
                characterActions.Jump();
            } 

            if (crouchPressed)
            {
                characterActions.Slide();
            }
            
            
            if (escPressed)
            {
                LevelSceneManager.Instance.TogglePause();
            }
        }

    }
}
