using System;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2Int gridMoveDirection; // Направление змейки.
    private Vector2Int gridPosition; // Позиция змейки.
    private float gridMoveTimer; // Время для автоматического премещения змейки.
    private float gridMoveTimerMax; // Её максимальное значение.
    private LevelGrid levelGrid; // Ссылка змеи, пометка.
    private int snakeBodySize; // Размер хвоста змеи.
    private List<Vector2Int> snakeMovePositinList; // 

    public void Setup(LevelGrid levelGrid) // Ссылка змеи, пометка.
    {
        this.levelGrid = levelGrid; // Ссылка змеи, пометка.
    }
    
    private void Awake() 
    {
        gridPosition = new Vector2Int(0, 0); // Позция змейки, 0.9 - z, чтобы объект отображался.
        gridMoveTimerMax = .5f; // Интервал движения.
        gridMoveTimer = gridMoveTimerMax; // Так надо.
        gridMoveDirection = new Vector2Int(1, 0); // Векторное управление змеи, вправо.

        snakeMovePositinList = new List<Vector2Int>(); // Инициализация списка.
        snakeBodySize = 0;
    }

    private void Update()
    {
        HandleInput();
        HandleGrindMovement();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) // Управление стрелками.
        {
            if (gridMoveDirection.y != -1) // Не позволяет поворачиваться на 180 градусов.
            {
                gridMoveDirection.x = 0;
                gridMoveDirection.y = +1;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (gridMoveDirection.y != +1)
            {
                gridMoveDirection.x = 0;
                gridMoveDirection.y = -1;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (gridMoveDirection.x != +1)
            {
                gridMoveDirection.x = -1;
                gridMoveDirection.y = 0;
            }
            
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (gridMoveDirection.x != -1)
            {
                gridMoveDirection.x = +1;
                gridMoveDirection.y = 0;
            }
        }
    }

    private void HandleGrindMovement()
    {
        gridMoveTimer += Time.deltaTime; // Обновление таймера каждый кадр.
        if (gridMoveTimer >= gridMoveTimerMax) // Если время >= максимального, то цикл выполняется.
        {
            gridPosition += gridMoveDirection; // Позиция + движение в сторону.
            
            bool snakeEatFood = levelGrid.TrySnakeEatFood(gridPosition); // Передаём сетке свою позицию.
            if (snakeEatFood) // if true - body+1
            {
                snakeBodySize++;
            }
            
            snakeMovePositinList.Insert(0, gridPosition);
            
            gridMoveTimer -= gridMoveTimerMax; // Не понял зачем.

            if (snakeMovePositinList.Count >= snakeBodySize + 1) // Если список больше размера змеи.
            {   // Удаление последнего элемента списка.
                snakeMovePositinList.RemoveAt(snakeMovePositinList.Count - 1);
            }

            for (int i = 0; i < snakeMovePositinList.Count; i++)
            {
                // Здесь должна быть реализована хуета того, как растёт хвост.
                // Но та макака хуету сделала - затычку.
            }
            
            transform.position = new Vector3
                (gridPosition.x, gridPosition.y); // Изменение позиции змейки.
            transform.eulerAngles = new Vector3
                (0, 0, GetAngleFromVector(gridMoveDirection) -90); // Изменение направления спрайта
                                            // по углу эйлера в взависимости от направления движения по Z.
                                            // -90 т.к начало змейки влево, а голова спрайта смотрит вверх.
        }
    }

    private float GetAngleFromVector(Vector2Int dir) // Метор определяющий направление спрайта.
    {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n > 0)
        {
            n += 360;
        }
        return n;
    }

    public Vector2Int GetGridPosition() // Метод, если кто-то запрашивает позицию змеи в сетке.
    {
        return gridPosition;
    }

    // Возвраает полный список позиций окупаемые змеёй: голова и хвост.
    public List<Vector2Int> GetFullSnakeGridPositionList()
    {
        List<Vector2Int> gridPositionList = new List<Vector2Int>() { gridPosition };
        gridPositionList.AddRange(snakeMovePositinList);
        return gridPositionList;
    }
}
