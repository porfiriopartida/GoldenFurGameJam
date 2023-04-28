using System;
using GoldenFur.Common;
using GoldenFur.ScriptableObjects;
using GoldenFur.ScriptableObjects.Primitives;
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

        public FloatValue MasterVolume;
        public FloatValue SFXVolume;
        public FloatValue MusicVolume;
        public AudioClip[] sampleBfxClips;
        
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
            UpdateMusicVolume();
        }

        public void UpdateMusicVolume()
        {
            backgroundAudioSource.volume = MusicVolume.value * MasterVolume.value;
            // Debug.Log($"UpdateMusicVolume: {backgroundAudioSource.volume}");
        }

        //SFX have multiple sources.
        public void UpdateSfxVolume(AudioSource src)
        {
            src.volume = SFXVolume.value * MasterVolume.value;
            // Debug.Log($"UpdateSfxVolume: {src.volume}");
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
            UpdateSfxVolume(src);
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

        public void ToggleAudio()
        {
            this.IsMusicOn.value = !this.IsMusicOn.value;
        }

        public void ToggleSfx()
        {
            this.IsSfxOn.value = !this.IsSfxOn.value;
        }

        public void PlayRandomClip()
        {
            if (this.defaultSfxAudioSource.isPlaying)
            {
                return;
            }

            this.PlaySfx(this.defaultSfxAudioSource, sampleBfxClips);
        }
    }
}
