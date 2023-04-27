using UnityEditor;
using UnityEngine;

namespace GoldenFur.Utils
{
    public class GoldenFurMenuUtils : Editor
    {
        [MenuItem( "GoldenFur/ResetTutorial" )] // CMD + SHIFT + W
        public static void ResetTutorial()
        {
            PlayerPrefs.SetInt("TutorialTaken", 0);
        }
    }
}