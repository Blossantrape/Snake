using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettingsSlider : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;

    private void Awake()
    {
        //transform.Find("settingsInside").Find("musicSlider").GetComponent<Slider>().onValueChanged.AddListener(SetMusicVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume(musicSlider.value);
        }
    }

    private void SetMusicVolume(float volume)
    {
        //volume = musicSlider.value;
        float mixerVolume = Mathf.Lerp(-80f, 0f, volume);
        myMixer.SetFloat("music", mixerVolume);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        
        SetMusicVolume(musicSlider.value);
    }
}
