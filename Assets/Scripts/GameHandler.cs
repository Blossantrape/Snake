using UnityEngine;

/// <summary>
/// Обработчик игры.
/// </summary>
public class GameHandler : MonoBehaviour
{
    private static GameHandler _instance;

    private static int _score = 0; // Счёт игры
    private readonly int _widthGh = 5;
    private readonly int _heightGh = 10;
    
    [SerializeField] private Snake snake;
    private LevelGrid _levelGrid;
    
    // Box
    private Box _box;
    private Transform _parentTransform;

    //[HideInInspector] private Camera _mainCamera;
    //private Camera _mainCamera;

    private void Awake() {
        _instance = this;
        //_mainCamera = Camera.main;
        InitializeStatic();
    }

    private void Start() {
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

    private static void InitializeStatic() {
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

    public static void SnakeDied() {
        GameOverWindow.ShowStatic();
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
}
