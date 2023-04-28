using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jugar : MonoBehaviour
{
    [SerializeField] private string NewGameLevel = "GameScene";
  public void NewGameButton()
    {
        SceneManager.LoadScene(NewGameLevel);
    }
}
