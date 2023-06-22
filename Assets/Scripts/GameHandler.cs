using System;
using UnityEngine;

/// <summary>
/// Обработчик игры.
/// </summary>
public class GameHandler : MonoBehaviour
{
    private static GameHandler _instance;

    private static int _score = 0; // Счёт игры
    [SerializeField] private int _widthGh = 5;
    [SerializeField] private int _heightGh = 10;
    
    [SerializeField] private Snake snake;
    private LevelGrid _levelGrid;

    private void Awake() {
        _instance = this;
        InitializeStatic();
    }

    private void Start() {
        // Параметры берутся из размера используемой сетки (можно Scale from background).
        _levelGrid = new LevelGrid(_widthGh, _heightGh);
        
        snake.Setup(_levelGrid); // Ссылка змеи, пометка.
        _levelGrid.Setup(snake); // Передача ссылки на змею.
        
        // этот еблан опять тут своё приписал, потом добавить загрузку сцены.
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
