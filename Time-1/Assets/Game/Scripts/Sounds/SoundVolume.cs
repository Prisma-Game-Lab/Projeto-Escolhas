using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider musicVolume;
    public Slider overallVolume;
    public Slider sfxVolume;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVol"))
            musicVolume.value = PlayerPrefs.GetFloat("musicVol");
        if (PlayerPrefs.HasKey("overallVol"))
            overallVolume.value = PlayerPrefs.GetFloat("overallVol");
        if (PlayerPrefs.HasKey("sfxVol"))
            sfxVolume.value = PlayerPrefs.GetFloat("sfxVol");
    }
    public void SetMusictrackVolume()
    {
        PlayerPrefs.SetFloat("musicVol", musicVolume.value);
        mixer.SetFloat("musicVol", Mathf.Log10(0.999f - musicVolume.value) * 25);
    }
    public void SetoveralltrackVolume()
    {
        PlayerPrefs.SetFloat("overallVol", overallVolume.value);
        mixer.SetFloat("overallVol", Mathf.Log10(0.999f - overallVolume.value) * 25);
    }
    public void SetsfxtrackVolume()
    {
        PlayerPrefs.SetFloat("sfxVol", sfxVolume.value);
        mixer.SetFloat("sfxVol", Mathf.Log10(0.999f - sfxVolume.value) * 25);
    }

}
