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

            var clothingPrefix = new string []
            {
                "head", "body", "leg"
            }
            ;
            for (int i = 0; i < clothingPrefix.Length; i++)
            {
                var currentPrefix = clothingPrefix[i];
                for (int j = 0; j < 4; j++)
                {
                    var clothingKey = currentPrefix + PlayerPrefKeys.Clothing + j;
                    var clothingResult = PlayerPrefs.GetInt(clothingKey, -1);

                    switch (clothingResult)
                    {
                        case 0:
                            Debug.Log($"El tribilin NO TIENE : {clothingKey} activo.");
                            break;
                        case 1:
                            Debug.Log($"El tribilin tiene : {clothingKey} activo.");
                            break;
                        default:
                            Debug.Log($"{clothingKey} NO EXISTE.");
                            break;
                    }
                }
            }
        }

    }
}