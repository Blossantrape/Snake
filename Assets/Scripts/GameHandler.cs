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
        Debug.Log("GameHandler.Start");

        // Параметры берутся из размера используемой сетки (можно Scale from background).
        levelGrid = new LevelGrid(3.32f, 6.02f); 
        
        snake.Setup(levelGrid); // Ссылка змеи, пометка.
        levelGrid.Setup(snake); // Передача ссылки на змею.
    }
}
