using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private string NewGameLevel = "Creditos";
    public void NewGameButton()
    {
        SceneManager.LoadScene(NewGameLevel);
    }
}
