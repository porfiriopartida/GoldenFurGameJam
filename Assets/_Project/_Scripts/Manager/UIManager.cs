using UnityEngine;
using TMPro;

namespace GoldenFur.Manager
{
    public class UIManager : MonoBehaviour
    {
        public string scoreFormat = "Score: {0}";
        public string coinsFormat = "{0}";
        public TextMeshProUGUI scoreLabel;
        public TextMeshProUGUI coinsLabel;
        
        private void Update()
        {
            scoreLabel.text = string.Format(scoreFormat, ScoreManager.Instance.currentScore);
            coinsLabel.text = string.Format(coinsFormat, ScoreManager.Instance.coinScore);
        }
    }
}