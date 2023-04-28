using System;
using GoldenFur.Manager;
using GoldenFur.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace GoldenFur.UI
{
    [ExecuteAlways]
    public class SliderVolumen : MonoBehaviour
    {
        public Slider sliderMaster;
        public Slider sliderMusica;
        public Slider sliderSFX;

        public FloatValue MasterVolume;
        public FloatValue MusicVolume;
        public FloatValue SFXVolume;

        private void OnEnable()
        {
            sliderMaster.value = MasterVolume.value;
            sliderMusica.value = MusicVolume.value;
            sliderSFX.value = SFXVolume.value;
        }

        private void Start()
        {
            sliderMaster.onValueChanged.AddListener(UpdateMasterVolume);
            sliderMusica.onValueChanged.AddListener(UpdateMusicVolume);
            sliderSFX.onValueChanged.AddListener(UpdateSFXVolume);
        }


        private void UpdateMusicVolume(float val)
        {
            Debug.Log("UpdateMusicVolume");
            MusicVolume.value = val;
            SoundManager.Instance.UpdateMusicVolume();
        }
        private void UpdateSFXVolume(float val)
        {
            Debug.Log("UpdateSFXVolume");
            SFXVolume.value = val;
            SoundManager.Instance.UpdateSfxVolume(SoundManager.Instance.defaultSfxAudioSource);
            
            SoundManager.Instance.PlayRandomClip();
        }
        private void UpdateMasterVolume(float val)
        {
            Debug.Log("UpdateMasterVolume");
            MasterVolume.value = val;
            SoundManager.Instance.UpdateMusicVolume();
            SoundManager.Instance.UpdateSfxVolume(SoundManager.Instance.defaultSfxAudioSource);
        }
    }
}
