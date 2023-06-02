using UnityEngine;

/// <summary>
/// Обработчик игры.
/// </summary>
public class GameHandler : MonoBehaviour
{
    [SerializeField] private Snake snake;
    private LevelGrid levelGrid;
    private void Start()
    {
        // Параметры берутся из размера используемой сетки (можно Scale from background).
        levelGrid = new LevelGrid(8, 15);
        
        snake.Setup(levelGrid); // Ссылка змеи, пометка.
        levelGrid.Setup(snake); // Передача ссылки на змею.
    }
}
