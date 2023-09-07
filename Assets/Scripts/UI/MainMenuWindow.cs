using GameLoader;
using General;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuWindow : MonoBehaviour
    {
        [SerializeField] private Transform _howToPlayInside;
        [SerializeField] private Transform _settingsInside;
        [SerializeField] private Transform _snakeText;
        [SerializeField] private CanvasGroup _background;
        //private float _timer;
        
        private enum Sub
        {
            MainMenu,
            HowToPlay,
            Settings,
            Back,
        }
        private void Awake()
        {
            transform.Find("buttonsMainMenu").GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            transform.Find("howToPlayInside").GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            transform.Find("settingsInside").GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            
            // Тут мы в дочерни ищем кнопку и через компонент кнопки по клику вызываем нужный функианал.
            transform.Find("buttonsMainMenu").Find("playBtn").GetComponent<Button>().onClick.AddListener(() => Loader.Load(Loader.Scene.GameScene));
            transform.Find("buttonsMainMenu").Find("playBtn").GetComponent<Button>().onClick.AddListener(() => SoundManager.PlaySound(SoundManager.Sound.ButtonClick));
        
            transform.Find("buttonsMainMenu").Find("howToPlayBtn").GetComponent<Button>().onClick.AddListener(() => ShowSub(Sub.HowToPlay));
            transform.Find("buttonsMainMenu").Find("howToPlayBtn").GetComponent<Button>().onClick.AddListener(() => SoundManager.PlaySound(SoundManager.Sound.ButtonClick));
        
            transform.Find("buttonsMainMenu").Find("settingsBtn").GetComponent<Button>().onClick.AddListener(() => ShowSub(Sub.Settings));
            transform.Find("buttonsMainMenu").Find("settingsBtn").GetComponent<Button>().onClick.AddListener(() => SoundManager.PlaySound(SoundManager.Sound.ButtonClick));
            transform.Find("settingsInside").Find("clearHighscoreBtn").GetComponent<Button>().onClick.AddListener(Score.ClearScore);
        
            transform.Find("buttonsMainMenu").Find("quitBtn").GetComponent<Button>().onClick.AddListener(() => Application.Quit());
            transform.Find("buttonsMainMenu").Find("quitBtn").GetComponent<Button>().onClick.AddListener(() => SoundManager.PlaySound(SoundManager.Sound.ButtonClick));
        
            // Back Buttons
            transform.Find("howToPlayInside").Find("backBtn").GetComponent<Button>().onClick.AddListener(() => ShowSub(Sub.Back));
            transform.Find("howToPlayInside").Find("backBtn").GetComponent<Button>().onClick.AddListener(() => SoundManager.PlaySound(SoundManager.Sound.ButtonClick));
            transform.Find("settingsInside").Find("backBtn").GetComponent<Button>().onClick.AddListener(() => ShowSub(Sub.Back));
            transform.Find("settingsInside").Find("backBtn").GetComponent<Button>().onClick.AddListener(() => SoundManager.PlaySound(SoundManager.Sound.ButtonClick));
        
            ShowSub(Sub.MainMenu);
        }

        private void ShowSub(Sub sub)
        {
            transform.Find("buttonsMainMenu").gameObject.SetActive(false);
            //transform.Find("howToPlayInside").gameObject.SetActive(false);
            //transform.Find("settingsInside").gameObject.SetActive(false);

            switch (sub)
            {
                case Sub.MainMenu:
                    transform.Find("howToPlayInside").gameObject.SetActive(false);
                    transform.Find("settingsInside").gameObject.SetActive(false);
                    transform.Find("buttonsMainMenu").gameObject.SetActive(true);
                    break;
                case Sub.HowToPlay:
                    transform.Find("buttonsMainMenu").gameObject.SetActive(true);
                    transform.Find("howToPlayInside").gameObject.SetActive(true);
                    _howToPlayInside.localPosition = new Vector2(0, -Screen.height);
                    _howToPlayInside.LeanMoveLocalY(0, 0.5f).setEaseOutExpo().delay = 0.1f;
                    _snakeText.LeanMoveLocalY(420, 0.5f).setEaseOutExpo().delay = 0.1f;
                    break;
                case Sub.Settings:
                    transform.Find("buttonsMainMenu").gameObject.SetActive(true);
                    transform.Find("settingsInside").gameObject.SetActive(true);
                    _settingsInside.localPosition = new Vector2(0, -Screen.height);
                    _settingsInside.LeanMoveLocalY(0, 0.5f).setEaseOutExpo().delay = 0.1f;
                    _snakeText.LeanMoveLocalY(420, 0.5f).setEaseOutExpo().delay = 0.1f;
                    break;
                case Sub.Back:
                    transform.Find("buttonsMainMenu").gameObject.SetActive(true);
                    _snakeText.LeanMoveLocalY(380, 0.5f).setEaseOutExpo().delay = 0.1f;
                    if (transform.Find("howToPlayInside").gameObject.activeInHierarchy)
                    {
                        _howToPlayInside.LeanMoveLocalY(-1400, 0.7f).setEaseOutExpo().delay = 0.1f;
                        _howToPlayInside.gameObject.SetActive(false);
                    }
                    else if (transform.Find("settingsInside").gameObject.activeInHierarchy)
                    {
                        _settingsInside.LeanMoveLocalY(-1400, 0.7f).setEaseOutExpo().delay = 0.1f;
                        _settingsInside.gameObject.SetActive(false);
                    }
                    break;
            }
        }
    }
}
