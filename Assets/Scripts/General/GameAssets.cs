using System;
using UnityEngine;

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
        //public AudioClip mainMenuMusicClip;
        //public AudioClip gameMusicClip;
    
        [Serializable]
        public class SoundAudioClip
        {
            public SoundManager.Sound sound;
            public AudioClip audioClip;
        }
    }
}
