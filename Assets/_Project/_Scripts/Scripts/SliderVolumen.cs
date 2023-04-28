using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderVolumen : MonoBehaviour
{
    public AudioMixer MasterVolume;
    public Slider sliderM;
    public Slider sliderMusica;
    public Slider sliderE;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MasterVolume.SetFloat("VolumenMaster", Mathf.Log(sliderM.value) * 20);
        MasterVolume.SetFloat("VolumenMaster", Mathf.Log(sliderMusica.value) * 20);
        MasterVolume.SetFloat("VolumenMaster", Mathf.Log(sliderE.value) * 20);
    }
}
