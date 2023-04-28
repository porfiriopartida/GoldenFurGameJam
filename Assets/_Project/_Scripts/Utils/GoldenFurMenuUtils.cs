using GoldenFur.Manager;
using UnityEditor;
using UnityEngine;

namespace GoldenFur.Utils
{
    public class GoldenFurMenuUtils : Editor
    {
        [MenuItem( "GoldenFur/ResetTutorial" )]
        public static void ResetTutorial()
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.Has_Tutorial_Run_Key, 0);
        }

        [MenuItem( "GoldenFur/DEBUG_PLAYER_PREFS" )]
        public static void DebugPlayerPrefs()
        {
            var hasTutorialRun = PlayerPrefs.GetInt(PlayerPrefKeys.Has_Tutorial_Run_Key, -1);
            Debug.Log($"[{PlayerPrefKeys.Has_Tutorial_Run_Key}]: {hasTutorialRun}");
        }

    }
}