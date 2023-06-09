using UnityEngine;

/// <summary>
/// Обработчик игры.
/// </summary>
public class GameHandler : MonoBehaviour
{
    private static GameHandler _instance;

    private static int _score;
    [SerializeField] private Snake snake;
    private LevelGrid _levelGrid;

    private void Awake() {
        _instance = this;
    }

    private void Start() {
        // Параметры берутся из размера используемой сетки (можно Scale from background).
        _levelGrid = new LevelGrid(8, 15);
        
        snake.Setup(_levelGrid); // Ссылка змеи, пометка.
        _levelGrid.Setup(snake); // Передача ссылки на змею.
    }

    // Запросить результат счёта
    public static int GetScore() {
        return _score;
    }

    // Добавить результат счёта
    public static void AddScore() {
        _score += 10000;
        Debug.Log("Add Score");
    }
}
