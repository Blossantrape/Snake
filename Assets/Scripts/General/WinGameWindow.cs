using System;
using GameLoader;
using General;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class WinGameWindow : MonoBehaviour
{
    private static WinGameWindow _instance;
    
    private void Awake()
    {
        _instance = this;
        transform.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        transform.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
        
        transform.Find("mainMenuBtnGWW").GetComponent<Button>().onClick.AddListener(() => SoundManager.PlaySound(SoundManager.Sound.ButtonClick));
        transform.Find("mainMenuBtnGWW").GetComponent<Button>().onClick.AddListener(() => Loader.Load(Loader.Scene.MainMenu));
        
        Hide();
    }
    
    private void Show() {
        gameObject.SetActive(true);
        SoundManager.PlaySound(SoundManager.Sound.WinGame);

        //Transform retryButton = transform.Find("retryButton");
        //retryButton.gameObject.SetActive(true);

        //transform.Find("scoreTextGOW").GetComponent<TextMeshProUGUI>().text = Score.GetScore().ToString();
        //transform.Find("highscoreTextGOW").GetComponent<TextMeshProUGUI>().text = "HIGHSCORE " + Score.GetHighscore();
            
        //transform.Find("ScoreWindow").gameObject.SetActive(false);
        ScoreWindow.HideStatic();
    }
    
    private void Hide() {
        gameObject.SetActive(false);
    }

    public static void ShowStatic() {
        _instance.Show();
    }

    /*public static void HideStatic() {
        _instance.Hide();
    }*/
}
