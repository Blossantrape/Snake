using System;
using General;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PauseBtn : MonoBehaviour
    {
        private static PauseBtn _instance;
    
        private void Awake()
        {
            _instance = this;
            
            transform.Find("pauseBtn").GetComponent<Button>().onClick.AddListener(() => SoundManager.PlaySound(SoundManager.Sound.ButtonClick));
            transform.Find("pauseBtn").GetComponent<Button>().onClick.AddListener(() => GameHandler.PauseGame());
        }
    }
}

