using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChangeGameSettings : MonoBehaviour
{
    public Slider _sfxSlider = null;
    public Slider _musicSlider = null;


    // void Start() {}

    public void ChangeSFXVolume()
    {
        if (GameSettings._instance != null && _sfxSlider != null)
        {
            GameSettings._instance._sfxVolume = _sfxSlider.value;
            if (AudioManager._instance != null)
            {
                if (AudioManager._instance._sfxSources.Count <= 0)
                    return;

                AudioManager._instance._sfxSources[0].volume = _sfxSlider.value;
                AudioManager._instance._sfxSources[0].PlayOneShot(AudioManager._instance._sfxSources[0].clip);
            }
        }
    }

    public void ChangeMusicVolume()
    {
        if (GameSettings._instance != null && _musicSlider != null)
        {
            GameSettings._instance._musicVolume = _musicSlider.value;
            if (AudioManager._instance != null)
            {
                foreach (AudioSource source in AudioManager._instance._musicSources)
                {
                    if (source.isPlaying)
                        source.volume = _musicSlider.value;
                }
            }
        }
    }
}
