using System;
using UnityEngine;

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
    
    [Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }
}
