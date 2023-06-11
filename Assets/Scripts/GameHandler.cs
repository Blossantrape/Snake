using UnityEngine;

/// <summary>
/// Обработчик игры.
/// </summary>
public class GameHandler : MonoBehaviour
{
    private static GameHandler _instance;

    private static int _score = 0; // Счёт игры
    
    [SerializeField] private Snake snake;
    private LevelGrid _levelGrid;

    private void Awake() {
        _instance = this;
        InitializeStatic();
    }

    private void Start() {
        // Параметры берутся из размера используемой сетки (можно Scale from background).
        _levelGrid = new LevelGrid(8, 15);
        
        snake.Setup(_levelGrid); // Ссылка змеи, пометка.
        _levelGrid.Setup(snake); // Передача ссылки на змею.
        
        // этот еблан опять тут своё приписал, потом добавить загрузку сцены.
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
}
