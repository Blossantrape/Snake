using System;
using UnityEngine;

namespace General
{
    public static class Score
    {
        public static event EventHandler OnHighscoreChanged; 

        private static int _score;
        
        public static void InitializeStatic()
        {
            OnHighscoreChanged = null;
            _score = 0;
        }
    
        // Запросить результат счёта
        public static int GetScore() {
            return _score;
        }

        // Добавить результат счёта
        public static void AddScore() {
            _score += 1;
            Debug.Log("Add Score");
        }
        
        public static int GetHighscore()
        {
            return PlayerPrefs.GetInt("highscore", 0);
        }

        public static bool TrySetNewHighscore()
        {
            return TrySetNewHighscore(_score);
        }

        private static bool TrySetNewHighscore(int score)
        {
            int highscore = GetHighscore();
            if (score > highscore)
            {
                PlayerPrefs.SetInt("highscore", score);
                PlayerPrefs.Save();
                if (OnHighscoreChanged != null)
                {
                    OnHighscoreChanged(null, EventArgs.Empty);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void ClearScore()
        {
            PlayerPrefs.SetInt("highscore", _score = 0);
            PlayerPrefs.Save();
            SoundManager.PlaySound(SoundManager.Sound.ButtonClick);
        }

        public static void DebugScore()
        {
            PlayerPrefs.SetInt("highscore", _score = 271);
            PlayerPrefs.Save();
            SoundManager.PlaySound(SoundManager.Sound.ButtonClick);
        }
    }
}