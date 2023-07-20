using UnityEngine;
using UnityEngine.UI;

namespace General
{
    public static class SoundManager
    {
        private static AudioSource _audioSource = null;
        public enum Sound
        {
            SnakeMove, //+
            SnakeDie, // Dead+
            SnakeEat, //+
            ButtonClick, // +
            // ButtonOver, // Хуета, убрать. Для наведения на кнопку.
            BackGroundMenu, //+
            BackGroundGame, //+
            RestartGame, //+
            GameOver,  //+
        }
        
        private static void Initialize()
        {
            // Ищем объект "Sounds" на сцене. Если его нет, создаем новый.
            GameObject soundGameObject = GameObject.Find("Sounds");
            if (soundGameObject == null)
            {
                soundGameObject = new GameObject("Sounds");
            }

            // Получаем или добавляем компонент AudioSource к объекту "Sounds"
            _audioSource = soundGameObject.GetComponent<AudioSource>();
            if (_audioSource == null)
            {
                _audioSource = soundGameObject.AddComponent<AudioSource>();
                _audioSource.playOnAwake = false;
                
            }

            // Помечаем объект "Sounds" чтобы он не уничтожался при переходе сцен
            Object.DontDestroyOnLoad(soundGameObject);
        }
    
        public static void PlaySound(Sound sound)
        {
            // Проверяем, что AudioSource был инициализирован
            if (_audioSource == null)
            {
                // Если _audioSource еще не инициализирован, вызываем Initialize()
                Initialize();
            }
            
            GameObject soundGameObject = new GameObject("Sounds");
            //AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            soundGameObject.GetComponent<AudioSource>();
            //_audioSource.playOnAwake = false;
            _audioSource.clip = GetAudioClip(sound);
            _audioSource.PlayOneShot(GetAudioClip(sound));
        
            Object.Destroy(soundGameObject, _audioSource.clip.length); // Удаление после поспросизведения.
            //soundGameObject.AddComponent<MyDontDestroyOnLoad>(); // Не позволяет объекту удалиться при переходе сцен.
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
