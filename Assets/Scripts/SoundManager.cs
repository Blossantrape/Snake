using UnityEngine;
using UnityEngine.UI;

public static class SoundManager
{
    public enum Sound
    {
        SnakeMove,
        SnakeDie, // Dead
        SnakeEat,
        ButtonClick,
        ButtonOver, // Хуета, убрать. Для наведения на кнопку.
        BackGroundMenu,
        BackGroundGame,
        RestartGame,
        GameOver,
    }
    
    public static void PlaySound(Sound sound)
    {
        GameObject soundGameObject = new GameObject("Sounds");
        AudioSource audioSource = soundGameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound));
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
        button.onClick += () => SoundManager.PlaySound(Sound.ButtonClick);
    }
}
