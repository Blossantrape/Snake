using System;
using GameLoader;
using General;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuWindow : MonoBehaviour
{
    private enum Sub
    {
        MainMenu,
        HowToPlay,
    }
    private void Awake()
    {
        transform.Find("buttonsMainMenu").GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        transform.Find("howToPlayInside").GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        // Тут мы в дочерни ищем кнопку и через компонент кнопки по клику вызываем нужный функианал.
        transform.Find("buttonsMainMenu").Find("playBtn").GetComponent<Button>().onClick.AddListener(() => Loader.Load(Loader.Scene.GameScene));
        transform.Find("buttonsMainMenu").Find("playBtn").GetComponent<Button>().onClick.AddListener(() => SoundManager.PlaySound(SoundManager.Sound.ButtonClick));
        //transform.Find("buttonsMainMenu").Find("playBtn").GetComponent<Button>().onClick.AddListener(() => SoundManager.AddButtonSound());
        
        Button playButton = transform.Find("buttonsMainMenu").Find("playBtn").GetComponent<Button>();
        playButton.onClick.AddListener(() => SoundManager.AddButtonSound(playButton));

        
        transform.Find("buttonsMainMenu").Find("howToPlayBtn").GetComponent<Button>().onClick.AddListener(() => ShowSub(Sub.HowToPlay));
        transform.Find("buttonsMainMenu").Find("howToPlayBtn").GetComponent<Button>().onClick.AddListener(() => SoundManager.PlaySound(SoundManager.Sound.ButtonClick));
        
        transform.Find("buttonsMainMenu").Find("quitBtn").GetComponent<Button>().onClick.AddListener(() => Application.Quit());
        transform.Find("buttonsMainMenu").Find("quitBtn").GetComponent<Button>().onClick.AddListener(() => SoundManager.PlaySound(SoundManager.Sound.ButtonClick));
        
        transform.Find("howToPlayInside").Find("backBtn").GetComponent<Button>().onClick.AddListener(() => ShowSub(Sub.MainMenu));
        transform.Find("howToPlayInside").Find("backBtn").GetComponent<Button>().onClick.AddListener(() => SoundManager.PlaySound(SoundManager.Sound.ButtonClick));
        
        ShowSub(Sub.MainMenu);
    }

    private void ShowSub(Sub sub)
    {
        transform.Find("buttonsMainMenu").gameObject.SetActive(false);
        transform.Find("howToPlayInside").gameObject.SetActive(false);

        switch (sub)
        {
            case Sub.MainMenu:
                transform.Find("howToPlayInside").gameObject.SetActive(false);
                transform.Find("buttonsMainMenu").gameObject.SetActive(true);
                break;
            case Sub.HowToPlay:
                transform.Find("buttonsMainMenu").gameObject.SetActive(false);
                transform.Find("howToPlayInside").gameObject.SetActive(true);
                break;
        }
    }
}
