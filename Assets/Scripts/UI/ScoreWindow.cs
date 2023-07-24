using General;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ScoreWindow : MonoBehaviour
    {
        private static ScoreWindow _instance;
        
        private TextMeshProUGUI _scoreText;

        private void Awake()
        {
            _instance = this;
            _scoreText = GameObject.FindGameObjectWithTag("scoreText").GetComponent<TextMeshProUGUI>();

            Score.OnHighscoreChanged += Score_OnHighscoreChanged;
            UpdateHighscore();
        }

        private void Score_OnHighscoreChanged(object sender, System.EventArgs e)
        {
            UpdateHighscore();
        }

        private void Update() {
            _scoreText.text = Score.GetScore().ToString();
        }

        private void UpdateHighscore()
        {
            int highscore = Score.GetHighscore();
            transform.Find("highscoreText").GetComponent<TextMeshProUGUI>().text = "HIGHSCORE\n" + highscore.ToString();
        }

        public static void HideStatic()
        {
            _instance.gameObject.SetActive(false);
        }
    }
}
