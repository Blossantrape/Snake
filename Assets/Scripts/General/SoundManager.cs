using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace General
{
    public static class SoundManager
    {
        private static AudioSource _audioSource = null;
        
        private static Dictionary<AudioMixerGroup, AudioSource> _audioSources = new Dictionary<AudioMixerGroup, AudioSource>();
        
        public enum Sound
        {
            SnakeMove, //+
            SnakeDie, // Dead+
            SnakeEat, //+
            ButtonClick, // +
            BackGroundMenu, //+
            BackGroundGame, //+
            RestartGame, //+
            GameOver,  //+
            WinGame, //+
        }
        
        private static void Initialize()
        {
            if (_audioSource == null)
            {
                GameObject soundGameObject = new GameObject("Sounds");
                _audioSource = soundGameObject.AddComponent<AudioSource>();
                _audioSource.outputAudioMixerGroup = GameAssets.I.sfxMixerGroup;
                _audioSource.playOnAwake = false;
                Object.DontDestroyOnLoad(soundGameObject);
            }
        }
    
        public static void PlaySound(Sound sound)
        {
            AudioMixerGroup mixerGroup = GetAudioMixerGroup(sound);

            // Получаем соответствующий AudioSource для этой группы
            AudioSource audioSource = GetAudioSourceForGroup(mixerGroup);

            // Воспроизводим звук
            audioSource.clip = GetAudioClip(sound);
            audioSource.PlayOneShot(audioSource.clip);
        }

        private static AudioMixerGroup GetAudioMixerGroup(Sound sound)
        {
            switch (sound)
            {
                case Sound.ButtonClick:
                    return GameAssets.I.buttonClickMixerGroup;
                case Sound.SnakeDie:
                    return GameAssets.I.deadMixerGroup;
                case Sound.SnakeEat:
                    return GameAssets.I.eatMixerGroup;
                case Sound.RestartGame:
                    return GameAssets.I.restartMixerGroup;
                case Sound.GameOver:
                    return GameAssets.I.gameOverMixerGroup;
                case Sound.BackGroundMenu:
                case Sound.BackGroundGame:
                    return GameAssets.I.musicMixerGroup;
                case Sound.WinGame:
                    return GameAssets.I.winGameMixerGroup;
                default:
                    return GameAssets.I.snakeMoveMixerGroup;
            }
        }
        
        private static AudioSource GetAudioSourceForGroup(AudioMixerGroup mixerGroup)
        {
            if (!_audioSources.TryGetValue(mixerGroup, out AudioSource audioSource))
            {
                GameObject soundGameObject = new GameObject("Sound-" + mixerGroup.name);
                audioSource = soundGameObject.AddComponent<AudioSource>();
                audioSource.outputAudioMixerGroup = mixerGroup;
                audioSource.playOnAwake = false;
                Object.DontDestroyOnLoad(soundGameObject);

                _audioSources[mixerGroup] = audioSource;
            }

            return audioSource;
        }
        
        private static AudioClip GetAudioClip(Sound sound)
        {
            foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.I.soundAudioClipArray)
            {
                if (soundAudioClip.sound == sound)
                {
                    return soundAudioClip.audioClip;
                }
            }
            Debug.LogError("Sound" + sound + "not found!");
            return null;
        }
        
        public static void AddButtonSound(this Button button)
        {
            button.onClick.AddListener(() => SoundManager.PlaySound(Sound.ButtonClick));
        }
        
        public static void PlayMainMenuMusic()
        {
            AudioClip mainMenuMusicClip = GameAssets.I.soundAudioClipArray[4].audioClip;
            GameObject mainMenuMusicObject = new GameObject("MainMenuBackgroundMusic");
            AudioSource mainMenuMusicSource = mainMenuMusicObject.AddComponent<AudioSource>();
            mainMenuMusicSource.clip = mainMenuMusicClip;
            mainMenuMusicSource.loop = true;
            mainMenuMusicSource.outputAudioMixerGroup = GameAssets.I.musicMixerGroup;
            mainMenuMusicSource.playOnAwake = false;
        
            mainMenuMusicSource.Play();
        }

        public static void PlayGameSceneMusic()
        {
            AudioClip gameBackgroundMusicClip = GameAssets.I.soundAudioClipArray[5].audioClip;
            GameObject gameBackgroundMusicObject = new GameObject("GameBackgroundMusic");
            AudioSource gameBackgroundMusicSource = gameBackgroundMusicObject.AddComponent<AudioSource>();
            gameBackgroundMusicSource.clip = gameBackgroundMusicClip;
            gameBackgroundMusicSource.loop = true;
            gameBackgroundMusicSource.outputAudioMixerGroup = GameAssets.I.musicMixerGroup;
            gameBackgroundMusicSource.playOnAwake = false;
        
            gameBackgroundMusicSource.Play();
        }

        public static void StopGameSceneMusic()
        {
            GameObject gameBackgroundMusicObject = GameObject.Find("GameBackgroundMusic");
            AudioSource gameBackgroundMusicSource = gameBackgroundMusicObject.GetComponent<AudioSource>();
            
            gameBackgroundMusicSource.Stop();
            Debug.Log("StopGameSceneMusic - true");
        }
    }
}
