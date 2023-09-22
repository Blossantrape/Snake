using System;
using GameLoader;
using General;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PauseWindow : MonoBehaviour
    {
        private static PauseWindow _instance;
        //private int _paramPauseGame = 1;
    
        private void Awake()
        {
            _instance = this;
        
            transform.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            transform.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
            
            //transform.Find("pauseBtn").GetComponent<Button>().onClick.AddListener(() => GameHandler.PauseGame(_paramPauseGame));
            
            //SoundManager.SetAudioMixerGroup(GameAssets.I.sfxMixerGroup);
            transform.Find("resumeBtnPW").GetComponent<Button>().onClick.AddListener(() => SoundManager.PlaySound(SoundManager.Sound.ButtonClick));
            transform.Find("resumeBtnPW").GetComponent<Button>().onClick.AddListener(() => GameHandler.ResumeGame());

            transform.Find("retryBtnPW").GetComponent<Button>().onClick.AddListener(() => SoundManager.PlaySound(SoundManager.Sound.RestartGame));
            transform.Find("retryBtnPW").GetComponent<Button>().onClick.AddListener(() => Loader.Load(Loader.Scene.GameScene));
            
            transform.Find("mainMenuBtnPW").GetComponent<Button>().onClick.AddListener(() => SoundManager.PlaySound(SoundManager.Sound.ButtonClick));
            transform.Find("mainMenuBtnPW").GetComponent<Button>().onClick.AddListener(() => Loader.Load(Loader.Scene.MainMenu));

            //_paramPauseGame = 0;
            
            Hide();
        }

        private void Show() {
            gameObject.SetActive(true);

            /*Transform pauseButton = transform.Find("pauseBtn");
            pauseButton.gameObject.SetActive(true);
        
            Debug.Log("Показ Retry/");*/
        }
    
        private void Hide() {
            gameObject.SetActive(false);
        }

        public static void ShowStatic() {
            _instance.Show();
        }

        public static void HideStatic()
        {
            _instance.Hide();
        }
    }
}
