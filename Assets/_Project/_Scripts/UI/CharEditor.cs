using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GoldenFur
{
    public class CharEditor : MonoBehaviour
    {
        [SerializeField] private string NewGameLevel = "GameScene";
    public void NewGameButton()
    {
        SceneManager.LoadScene(NewGameLevel);
    }
    }
}
