using System;
using UnityEngine;

public class MainMenuWindow : MonoBehaviour
{
    private void Awake()
    {
        // кастыль ебаный
        transform.Find("playBtn"); //clickfund = Loader.Load(Loader.Scene.GameScene);
        
        transform.Find("quitBtn"); //clickfund = Application.Quit();
    }
}
