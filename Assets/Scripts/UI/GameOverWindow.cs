using GameLoader;
using General;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameOverWindow : MonoBehaviour
    {
        private static GameOverWindow _instance;
    
        private void Awake()
        {
            _instance = this;
        
            transform.Find("retryButton").GetComponent<Button>().onClick.AddListener(Retry);
        
            Hide();
        }
    
        private void Show(bool isNewHighscore) {
            gameObject.SetActive(true);
            SoundManager.PlaySound(SoundManager.Sound.SnakeDie);

            Transform retryButton = transform.Find("retryButton");
            retryButton.gameObject.SetActive(true);
            //Debug.Log("Показ Retry/");
            
            transform.Find("newHighscoreTextGOW").gameObject.SetActive(isNewHighscore);

            transform.Find("scoreTextGOW").GetComponent<TextMeshProUGUI>().text = Score.GetScore().ToString();
            transform.Find("highscoreTextGOW").GetComponent<TextMeshProUGUI>().text = "HIGHSCORE " + Score.GetHighscore();
            
            //transform.Find("ScoreWindow").gameObject.SetActive(false);
            ScoreWindow.HideStatic();
        }

        private void Hide() {
            gameObject.SetActive(false);
        }

        public static void ShowStatic(bool isNewHighscore) {
            _instance.Show(isNewHighscore);
        }

        public void Retry()
        {
            SoundManager.PlaySound(SoundManager.Sound.RestartGame);
            Loader.Load(Loader.Scene.GameScene);
            // Show();
        }
    }
}
