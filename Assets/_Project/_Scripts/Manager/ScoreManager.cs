using GoldenFur.Common;

namespace GoldenFur.Manager
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        public float coinScore;
        public float currentScore;
        public void AddCoin(float amount = 1)
        {
            this.coinScore++;
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