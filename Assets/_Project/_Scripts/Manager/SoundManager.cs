using GoldenFur.Common;
using GoldenFur.ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GoldenFur.Manager
{
    public class SoundManager : Singleton<SoundManager>
    {
        public BooleanValue IsMusicOn;
        public BooleanValue IsSfxOn;
        
        [Header("Play non audio source specific SFX using this one, will pause playing ones if a new one arrives.")]
        public AudioSource defaultSfxAudioSource;
        public AudioSource backgroundAudioSource;
        
        //Loop Background Music
        public AudioClip[] bgClips;
        
        private int _activeClip = 0;

        private AudioClip NextClip()
        {
            _activeClip++;
            if (_activeClip >= bgClips.Length)
            {
                _activeClip = 0;
            }

            return bgClips[_activeClip];
        }

        void Update()
        {
            if (!IsMusicOn.value)
            {
                if (backgroundAudioSource.isPlaying)
                {
                    Pause();
                }
                return;
            }
            if (!backgroundAudioSource.isPlaying)
            {
                backgroundAudioSource.clip = NextClip();
                Resume();
            }
        }

        public void Pause()
        {
            backgroundAudioSource.Pause();
        }
        public void Resume()
        {
            backgroundAudioSource.Play();
        }

        public void PlayBackgroundMusic(AudioClip clip)
        {
            if (!IsMusicOn.value)
            {
                return;
            }

            backgroundAudioSource.clip = clip;
            Resume();
        }

        public void PlaySfx(AudioSource src, AudioClip clip)
        {
            if (!IsSfxOn.value)
            {
                return;
            }

            src.clip = clip;
            src.Play();
        }
        public void PlaySfx(AudioSource src, AudioClip[] clips)
        {
            this.PlaySfx(src, clips[Random.Range(0, clips.Length)]);
        }

        public void PlaySfx(AudioClip clip)
        {
            this.PlaySfx(this.defaultSfxAudioSource, clip);
        }
        public void PlaySfx(AudioClip[] clips)
        {
            this.PlaySfx(this.defaultSfxAudioSource, clips);
        }
    }
}
