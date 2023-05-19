using System;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector3Int gridMoveDirection; // Направление змейки.
    private Vector3Int gridPosition; // Позиция змейки.
    private float gridMoveTimer; // Время для автоматического премещения змейки.
    private float gridMoveTimerMax; // Её максимальное значение.
    
    private void Awake() 
    {
        gridPosition = new Vector3Int(10, 10, 1); // Позция змейки.
        gridMoveTimerMax = .5f; // Интервал движения.
        gridMoveTimer = gridMoveTimerMax; // Так надо.
        gridMoveDirection = new Vector3Int(1, 0, 0); // Векторное управление змеи, вправо.
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
            gridMoveTimer -= gridMoveTimerMax; // Не понял зачем.
            
            transform.position = new Vector3
                (gridPosition.x, gridPosition.y, gridPosition.z); // Изменение позиции змейки.
            transform.eulerAngles = new Vector3
                (0, 0, GetAngleFromVector(gridMoveDirection) -90); // Изменение направления спрайта
                                            // по углу эйлера в взависимости от направления движения по Z.
        }
    }

    private float GetAngleFromVector(Vector3Int dir) // Метор определяющий направление спрайта.
    {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n > 0)
        {
            n += 360;
        }
        return n;
    }
}
