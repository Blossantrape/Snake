using GameLoader;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace General
{
    public class WinGameWindow : MonoBehaviour
    {
        private static WinGameWindow _instance;
        [SerializeField] private VFXController _vfxController;
        [SerializeField] private Snake _snake;
    
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
            SoundManager.StopGameSceneMusic();
            ScoreWindow.HideStatic();
            _snake._state = Snake.State.Stop;
            _vfxController.ActivateVFX();
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
