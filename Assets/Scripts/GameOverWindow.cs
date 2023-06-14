using System;
using UnityEngine;

public class GameOverWindow : MonoBehaviour
{
    private static GameOverWindow _instance;
    
    private void Awake() {
        // опять ебаный кастыль
        // transform.Find("retryButton").GetComponent<>().ClickFunk = () Loader.Load(Loader.Scene.GameScene);
        
        Hide();
    }
    
    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    public static void ShowStatic() {
        _instance.Show();
    }
}
