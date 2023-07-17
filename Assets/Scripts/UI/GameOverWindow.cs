using GameLoader;
using General;
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
    
        private void Show() {
            gameObject.SetActive(true);
            SoundManager.PlaySound(SoundManager.Sound.SnakeDie);

            Transform retryButton = transform.Find("retryButton");
            retryButton.gameObject.SetActive(true);
        
            Debug.Log("Показ Retry/");
        }

        private void Hide() {
            gameObject.SetActive(false);
        }

        public static void ShowStatic() {
            _instance.Show();
        }

        public void Retry()
        {
            SoundManager.PlaySound(SoundManager.Sound.RestartGame);
            Loader.Load(Loader.Scene.GameScene);
            // Show();
        }
    }
}
