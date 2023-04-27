using UnityEngine;
using UnityEngine.SceneManagement;

namespace GoldenFur.Manager
{
    public class LevelSceneManager : MonoBehaviour
    {
        //SO?
        public float scoreAmount;
        public float scoreUpDelay;
        //Delay to increase score just by existing.
        private float _nextScoreCheck = 0;
        private void Start()
        {
            _nextScoreCheck = scoreUpDelay;
            
            //TODO: Check if order of execution affects this (ScoreManager must load before scene manager)
            //Reset score and coins
            ScoreManager.Instance.ResetScore();
        }
        private void Update()
        {
            if (_nextScoreCheck <= 0 )
            {
                ScoreManager.Instance.AddScore(scoreAmount);
                _nextScoreCheck = scoreUpDelay;
            }

            _nextScoreCheck -= Time.deltaTime;
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}