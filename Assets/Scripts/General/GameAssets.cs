using System;
using UnityEngine;
using UnityEngine.Audio;

namespace General
{
    public class GameAssets : MonoBehaviour
    {
        public static GameAssets I;

        private void Awake()
        {
            I = this;
        }

        public Sprite snakeHeadSprite;
        public Sprite snakeBodySprite;
        public Sprite foodSprite;
        public Sprite boxSprite;

        public SoundAudioClip[] soundAudioClipArray;
        
        public AudioMixerGroup sfxMixerGroup;
        public AudioMixerGroup musicMixerGroup;
        public AudioMixerGroup snakeMoveMixerGroup;
        
        [Serializable]
        public class SoundAudioClip
        {
            public SoundManager.Sound sound;
            public AudioClip audioClip;
        }
    }
}
