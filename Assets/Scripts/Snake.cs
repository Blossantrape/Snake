using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2Int gridMoveDirection; // Направление змейки.
    private Vector2Int gridPosition; // Позиция змейки.
    private float gridMoveTimer; // Время для автоматического премещения змейки.
    private float gridMoveTimerMax; // Её максимальное значение.
    private LevelGrid levelGrid; // Ссылка змеи, пометка.

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
                (gridPosition.x, gridPosition.y); // Изменение позиции змейки.
            transform.eulerAngles = new Vector3
                (0, 0, GetAngleFromVector(gridMoveDirection) -90); // Изменение направления спрайта
                                            // по углу эйлера в взависимости от направления движения по Z.
                                            // -90 т.к начало змейки влево, а голова спрайта смотрит вверх.
            levelGrid.SnakeMoved(gridPosition); // Передаём сетке свою позицию.
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
}
