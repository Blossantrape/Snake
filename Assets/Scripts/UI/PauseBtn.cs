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

        private void Show()
        {
            gameObject.SetActive(true);
        }
        
        private void Hide() {
            gameObject.SetActive(false);
        }

        public static void ShowStatic() {
            _instance.Show();
        }
        
        public static void HideStatic() {
            _instance.Hide();
        }
    }
}

