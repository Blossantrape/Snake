using UnityEngine;
using UnityEngine.UI;

namespace General
{
    public static class SoundManager
    {
        public enum Sound
        {
            SnakeMove, //+
            SnakeDie, // Dead+
            SnakeEat, //+
            ButtonClick, 
            // ButtonOver, // Хуета, убрать. Для наведения на кнопку.
            BackGroundMenu, //+
            BackGroundGame, //+
            RestartGame, 
            GameOver,
        }
    
        public static void PlaySound(Sound sound)
        {
            GameObject soundGameObject = new GameObject("Sounds");
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            soundGameObject.GetComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.clip = GetAudioClip(sound);
            audioSource.PlayOneShot(GetAudioClip(sound));
        
            Object.Destroy(soundGameObject, audioSource.clip.length); // Удаление после поспросизведения.
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
    }
}
