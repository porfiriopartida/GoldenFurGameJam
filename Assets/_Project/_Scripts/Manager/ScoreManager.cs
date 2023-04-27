using GoldenFur.Common;
using UnityEngine;

namespace GoldenFur.Manager
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        [Header("How much points would a coin award")]
        public float coinScoreAmount;
        
        [Header("Score counters, reset every level using ResetScore()")]
        public float coinScore;
        public float currentScore;
        public void AddCoin(float amount = 1)
        {
            this.coinScore++;
            this.currentScore += coinScoreAmount;
        }

        public void ResetScore()
        {
            this.currentScore = 0;
            this.coinScore = 0;
        }

        public void AddScore(float scoreAmount)
        {
            this.currentScore += scoreAmount;
        }
    }
}