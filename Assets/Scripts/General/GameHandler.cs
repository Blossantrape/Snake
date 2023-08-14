using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace General
{
    /// <summary>
    /// Обработчик игры.
    /// </summary>
    public class GameHandler : MonoBehaviour
    {
        private static GameHandler _instance;
        
        private readonly int _widthGh = 16;
        private readonly int _heightGh = 22;
    
        [SerializeField] private Snake snake;
        private LevelGrid _levelGrid;
    
        // Box
        private Box _box;
        private Transform _parentTransform;

        private void Awake() {
            _instance = this;
            Score.InitializeStatic();
            Time.timeScale = 1f;
        }

        private void Start() {
            // Воспроизведение фоновой музики от сцены.
            if (IsMainMenuScene())
            {
                SoundManager.PlayMainMenuMusic();
                //PlayMainMusic();
                Debug.Log("Menu music");
            }
            else if(IsGameScene())
            {
                SoundManager.PlayGameSceneMusic();
                //PlayGameMusic();
                Debug.Log("Game music");
            }
            
            // Параметры берутся из размера используемой сетки (можно Scale from background).
            _levelGrid = new LevelGrid(_widthGh, _heightGh);
            //_levelGrid.SetParent(transform);
        
            snake.Setup(_levelGrid); // Ссылка змеи, пометка.
            _levelGrid.Setup(snake); // Передача ссылки на змею.
        
            /*// Box Для проверик размера игрового поля.
            // Добавление компонента к объекту.
            _box = gameObject.AddComponent<Box>();
            // Создание границы игрового поля.
            _box.CreateBorder(_widthGh, _heightGh);*/
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                if (IsGamePaused()) {
                    GameHandler.ResumeGame();
                }
                else {
                    GameHandler.PauseGame();
                }
            }

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                Score.DebugScore();
                Debug.Log("251");
            }

            if (Score.GetScore() == 251)
            {
                WinGame();
                Debug.Log("Win");
            }
        }
        
        private bool IsMainMenuScene()
        {
            return SceneManager.GetActiveScene().name == "MainMenu";
        }

        private bool IsGameScene()
        {
            return SceneManager.GetActiveScene().name == "GameScene";
        }
        
        public static void SnakeDied()
        {
            bool isNewHighscore = Score.TrySetNewHighscore();
            GameOverWindow.ShowStatic(isNewHighscore);
            ScoreWindow.HideStatic();
        }

        public static void ResumeGame()
        {
            PauseWindow.HideStatic();
            Time.timeScale = 1f;
        }

        private static void PauseGame()
        {
            PauseWindow.ShowStatic();
            Time.timeScale = 0f;
        }

        private static bool IsGamePaused()
        {
            return Time.deltaTime == 0f;
        }

        private static void WinGame()
        {
            WinGameWindow.ShowStatic();
            Time.timeScale = 0f;
        }
    }
}
