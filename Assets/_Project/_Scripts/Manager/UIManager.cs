using System;
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

        public Transform musicOnToggleIcon;
        public Transform sfxOnToggleIcon;

        private void Start()
        {
            SyncUI();
        }
        public void ToggleAudio()
        {
            SoundManager.Instance.ToggleAudio();
            SyncUI();
        }
        private void SyncUI()
        {
            musicOnToggleIcon.gameObject.SetActive(SoundManager.Instance.IsMusicOn.value);
            sfxOnToggleIcon.gameObject.SetActive(SoundManager.Instance.IsSfxOn.value);
        }
        public void ToggleSfx()
        {
            SoundManager.Instance.ToggleSfx();
            SyncUI();
        }

        private void Update()
        {
            scoreLabel.text = string.Format(scoreFormat, ScoreManager.Instance.currentScore);
            coinsLabel.text = string.Format(coinsFormat, ScoreManager.Instance.coinScore);
        }
    }
}