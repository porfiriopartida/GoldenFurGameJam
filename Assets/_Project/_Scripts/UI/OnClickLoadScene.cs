using UnityEngine;
using UnityEngine.SceneManagement;

namespace GoldenFur.UI
{
    public class OnClickLoadScene : MonoBehaviour
    {
        [SerializeField] private string NewGameLevel = "GameScene";
        public void LoadScene()
        {
            SceneManager.LoadScene(NewGameLevel);
        }
    }
}
