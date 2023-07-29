using UI;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace General
{
    /// <summary>
    /// Обработчик игры.
    /// </summary>
    public class GameHandler : MonoBehaviour
    {
        private static GameHandler _instance;

        public AudioMixerGroup _sfxMixerGroup;
        
        private readonly int _widthGh = 16;
        private readonly int _heightGh = 22;
    
        [SerializeField] private Snake snake;
        private LevelGrid _levelGrid;

        //public AudioSource mainMenuMusicSource;
        //public AudioSource gameMusicSource;
    
        // Box
        private Box _box;
        private Transform _parentTransform;

        //[HideInInspector] private Camera _mainCamera;
        //private Camera _mainCamera;

        private void Awake() {
            _instance = this;
            //_mainCamera = Camera.main;
            //AudioMixerGroup sfxMixerGroup 
            Score.InitializeStatic();
            Time.timeScale = 1f;

            /*PlayerPrefs.SetInt("highscore", 100);
            PlayerPrefs.Save();
            Debug.Log(PlayerPrefs.GetInt("highscore"));*/
        }

        private void Start() {
            // Воспроизведение фоновой музики от сцены.
            if (IsMainMenuScene())
            {
                SoundManager.SetAudioMixerGroup(_sfxMixerGroup);
                //SoundManager.PlaySound(SoundManager.Sound.BackGroundMenu);
                PlayMainMusic();
                Debug.Log("Menu music");
            }
            else if(IsGameScene())
            {
                SoundManager.SetAudioMixerGroup(_sfxMixerGroup);
                //SoundManager.PlaySound(SoundManager.Sound.BackGroundGame);
                PlayGameMusic();
                Debug.Log("Game music");
            }
            
            // Параметры берутся из размера используемой сетки (можно Scale from background).
            _levelGrid = new LevelGrid(_widthGh, _heightGh);
            //_levelGrid.SetParent(transform);
        
            snake.Setup(_levelGrid); // Ссылка змеи, пометка.
            _levelGrid.Setup(snake); // Передача ссылки на змею.
            //AdjustCameraToGrid();
        
            // Box
            // Добавление компонента к объекту.
            _box = gameObject.AddComponent<Box>();
            // Создание границы игрового поля.
            _box.CreateBorder(_widthGh, _heightGh);
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
        }

        /// <summary>
        /// Регулирование камеры по сетке.
        /// </summary>
        /*private void AdjustCameraToGrid()
    {
        if (_mainCamera == null)
        {
            Debug.LogWarning("Main camera not found.");
            return;
        }

        float gridWidth = _levelGrid._width;
        float gridHeight = _levelGrid._height;

        float aspectRatio = (float) Screen.width / Screen.height;
        float cameraHeight = gridHeight + 2f;
        float cameraWidth = cameraHeight * aspectRatio;

        _mainCamera.orthographicSize = cameraHeight / 2f;

        Vector3 cameraPosition = new Vector3(gridWidth / 2f, gridHeight / 2f, -10f);
        _mainCamera.transform.position = cameraPosition;
    }*/

        private bool IsMainMenuScene()
        {
            /*if (Loader.Scene.MainMenu != Loader.Scene.GameScene)
            {
                return true;
            }

            return false;*/
            return SceneManager.GetActiveScene().name == "MainMenu";
        }

        private bool IsGameScene()
        {
            /*if (Loader.Scene.GameScene != Loader.Scene.MainMenu)
            {
                return true;
            }

            return false;*/
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

        public static void PauseGame()
        {
            PauseWindow.ShowStatic();
            Time.timeScale = 0f;
        }

        public static bool IsGamePaused()
        {
            return Time.deltaTime == 0f;
        }

        private void PlayMainMusic()
        {
            AudioClip mainMenuMusicClip = GameAssets.I.soundAudioClipArray[4].audioClip;
            GameObject mainMenuMusicObject = new GameObject("mainMenuBackgroundMusic");
            //AudioSource mainMenuMusicSource = SoundManager.PlaySound(SoundManager.Sound.BackGroundMenu);
            AudioSource mainMenuMusicSource = mainMenuMusicObject.AddComponent<AudioSource>();
            mainMenuMusicObject.GetComponent<AudioSource>();
            mainMenuMusicSource.clip = mainMenuMusicClip;
            mainMenuMusicSource.loop = true;
            mainMenuMusicSource.playOnAwake = false;
        
            mainMenuMusicSource.Play();
        }

        private void PlayGameMusic()
        {
            AudioClip gameBackgroundMusicClip = GameAssets.I.soundAudioClipArray[5].audioClip;
            GameObject gameBackgroundMusicMusicObject = new GameObject("GameBackgroundMusic");
            //AudioSource mainMenuMusicSource = SoundManager.PlaySound(SoundManager.Sound.BackGroundMenu);
            AudioSource gameBackgroundMusicSource = gameBackgroundMusicMusicObject.AddComponent<AudioSource>();
            gameBackgroundMusicMusicObject.GetComponent<AudioSource>();
            gameBackgroundMusicSource.clip = gameBackgroundMusicClip;
            gameBackgroundMusicSource.loop = true;
            gameBackgroundMusicSource.playOnAwake = false;
        
            gameBackgroundMusicSource.Play();
        }
    }
}
