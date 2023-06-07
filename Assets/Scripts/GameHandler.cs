using System;
using UnityEngine;

/// <summary>
/// Обработчик игры.
/// </summary>
public class GameHandler : MonoBehaviour
{
    private static GameHandler instance;

    private static int score;
    [SerializeField] private Snake snake;
    private LevelGrid levelGrid;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        // Параметры берутся из размера используемой сетки (можно Scale from background).
        levelGrid = new LevelGrid(8, 15);
        
        snake.Setup(levelGrid); // Ссылка змеи, пометка.
        levelGrid.Setup(snake); // Передача ссылки на змею.
    }

    public static int GetScore() {
        return score;
    }

    public static void AddScore() {
        score += 1;
        Debug.Log("Add Score");
    }
}
