using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundVolume : MonoBehaviour
{
    public AudioMixer mixer;
	public Slider musicVolume;

	public void SetSoundtrackVolume() {
		mixer.SetFloat("musicVol", Mathf.Log10(musicVolume.value)*20);
	}
    
}
