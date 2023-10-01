using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace General
{
    public class VolumeSettingsSlider : MonoBehaviour
    {
        [SerializeField] private AudioMixer myMixer;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider sfxSlider;

        private void Awake()
        {
            musicSlider.onValueChanged.AddListener(delegate { SetVolume(musicSlider, "music") ;});
            sfxSlider.onValueChanged.AddListener(delegate { SetVolume(sfxSlider, "sfx"); });
        }

        private void Start()
        {
            if (PlayerPrefs.HasKey("musicVolume"))
            {
                LoadVolume();
            }
            else
            {
                SetVolume(musicSlider, "music");
                SetVolume(sfxSlider, "sfx");
            }
        }

        private void SetVolume(Slider slider, string mixerParameter)
        {
            float volume = slider.value;
            float mixerVolume = Mathf.Lerp(-80f, 0f, volume);
            myMixer.SetFloat(mixerParameter, mixerVolume);
            PlayerPrefs.SetFloat(mixerParameter + "Volume", volume);
        }

        private void LoadVolume()
        {
            musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
            SetVolume(musicSlider, "music");
            sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
            SetVolume(sfxSlider, "sfx");
        }
    }
}
