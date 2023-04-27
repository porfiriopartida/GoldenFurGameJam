using System.Collections;
using GoldenFur.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GoldenFur.Manager
{
    
    public class LevelSceneManager : Singleton<LevelSceneManager>
    {
        [Header("Win Condition")]
        public float winScore = 100f;
        [Header("Time in seconds to reload the game after win/lose condition is met")]
        public float gameOverReloadDelay = 3f;
        
        //HP may sound like a player thing but in this case is a level thing, move if feels awkward codewise.
        [Header("HP Counters")]
        public int maxHp = 3;
        public int hp = 1;

        //Show/Hide Monster thresholds.
        public int monsterShowsOnHp = 1;
        public int monsterHidesOnHp = 3;
        
        //Delays and Score counters
        [Header("Score Counters")]
        public float scoreAmount;
        public float scoreUpDelay;
        public float hpUpDelay;
        //Delay to increase score just by existing.
        private float _nextScoreCheck = 0;
        private float _nextHpUp = 0;
        private void Start()
        {
            _nextScoreCheck = scoreUpDelay;
            
            //TODO: Check if order of execution affects this (ScoreManager must load before scene manager)
            //Reset score and coins
            ScoreManager.Instance.ResetScore();
            
            ShowMonster();
            Resume();
        }

        private void Resume()
        {
            Time.timeScale = 1;
        }
        private void Pause()
        {
            Time.timeScale = 0;
        }

        private void Update()
        {
            //Checks if the win condition was met last frame.
            CheckWinCondition();
            
            if (_nextScoreCheck < 0 )
            {
                ScoreManager.Instance.AddScore(scoreAmount);
                _nextScoreCheck = scoreUpDelay;
            }

            if (_nextHpUp < 0 && hp < maxHp)
            {
                IncreasePlayerHp();
            }

            _nextHpUp -= Time.deltaTime;
            _nextScoreCheck -= Time.deltaTime;
            
        }

        private void CheckWinCondition()
        {
            if (ScoreManager.Instance.currentScore >= winScore)
            {
                GameOver(true);
            }
        }

        private void IncreasePlayerHp()
        {
            //Hide the chasing monster as well if threshold is met.
            hp++;
            _nextHpUp = hpUpDelay;

            if (hp==monsterHidesOnHp)
            {
                HideMonster();
            }
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void PlayerHit()
        {
            hp--;
            _nextHpUp = hpUpDelay;

            if (hp < 0)
            {
                //Game Over.
                hp = 0;
                GameOver(false);
            }

            if (hp == monsterShowsOnHp)
            {
                ShowMonster();
            }
        }

        private void HideMonster()
        {
            Debug.Log("Hiding monster.");
        }

        private void ShowMonster()
        {
            Debug.Log("Showing monster.");
        }

        private void GameOver(bool isWinCondition)
        {
            if (isWinCondition)
            {
                Debug.Log("You win!");
            }
            else
            {
                Debug.Log("You lose!");
            }

            Pause();

            RestartAfterSeconds(gameOverReloadDelay);
        }

        private void RestartAfterSeconds(float f)
        {
            StartCoroutine(WaitForRealSeconds(f));
        }
 
        IEnumerator WaitForRealSeconds (float seconds) {
            var startTime = Time.realtimeSinceStartup;
            while (Time.realtimeSinceStartup-startTime < seconds) {
                yield return null;
            }
            Restart();
        }
    }
}