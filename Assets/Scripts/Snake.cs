using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Snake : MonoBehaviour
{
    // Структура направлений змеи.
    private enum Direction // Варианты направления для спрайта тела.
    {
        Left,
        Right,
        Up,
        Down
    }
    
    // Структура состояний змеи.
    private enum State
    {
        Alive,
        Dead
    }

    private State state; //  Состояние змеи.
    private Direction gridMoveDirection; // Направление змейки.
    private Vector2Int gridPosition; // Позиция змейки.
    private float gridMoveTimer; // Время для автоматического премещения змейки.
    private float gridMoveTimerMax; // Её максимальное значение.
    private LevelGrid levelGrid; // Ссылка змеи, пометка.
    private int snakeBodySize; // Размер хвоста змеи.
    private List<SnakeMovePosition> snakeMovePositinList; // 
    private List<SnakeBodyPart> snakeBodyPartList;

    public void Setup(LevelGrid levelGrid) // Ссылка змеи, пометка.
    {
        this.levelGrid = levelGrid; // Ссылка змеи, пометка.
    }
    
    private void Awake() 
    {
        gridPosition = new Vector2Int(0, 0); // Позция змейки, 0.9 - z, чтобы объект отображался.
        gridMoveTimerMax = .5f; // Интервал движения.
        gridMoveTimer = gridMoveTimerMax; // Так надо.
        gridMoveDirection = Direction.Right; // Направление змеи, вправо.

        snakeMovePositinList = new List<SnakeMovePosition>(); // Инициализация списка.
        snakeBodySize = 0; // Размер змеи
        snakeBodyPartList = new List<SnakeBodyPart>(); // Инициализация списка.

        state = State.Alive; // Стандартное живое состояние.
    }

    private void Update()
    {
        // Изменение режима игры в зависимости от состояния.
        switch (state)
        {
            case State.Alive:
                HandleInput();
                HandleGrindMovement();
                break;
            case State.Dead:
                break;
        }
        
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) // Управление стрелками.
        {
            if (gridMoveDirection != Direction.Down) // Не позволяет поворачиваться на 180 градусов.
            {
                gridMoveDirection = Direction.Up;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (gridMoveDirection != Direction.Up) // Не позволяет поворачиваться на 180 градусов.
            {
                gridMoveDirection = Direction.Down;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (gridMoveDirection != Direction.Right) // Не позволяет поворачиваться на 180 градусов.
            {
                gridMoveDirection = Direction.Left;
            }
            
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (gridMoveDirection != Direction.Left) // Не позволяет поворачиваться на 180 градусов.
            {
                gridMoveDirection = Direction.Right;
            }
        }
    }

    private void HandleGrindMovement()
    {
        gridMoveTimer += Time.deltaTime; // Обновление таймера каждый кадр.
        if (gridMoveTimer >= gridMoveTimerMax) // Если время >= максимального, то цикл выполняется.
        {
            gridMoveTimer -= gridMoveTimerMax; // Не понял зачем.

            SnakeMovePosition previousSnakeMovePosition = null;
            if (snakeMovePositinList.Count > 0)
            {
                previousSnakeMovePosition = snakeMovePositinList[0];
            }
            SnakeMovePosition snakeMovePosition = new SnakeMovePosition(previousSnakeMovePosition, gridPosition, gridMoveDirection);
            snakeMovePositinList.Insert(0, snakeMovePosition);

            Vector2Int gridMoveDirectionVector;
            switch (gridMoveDirection)
            {
                default:
                    case Direction.Right: gridMoveDirectionVector = new Vector2Int(+1, 0); break;
                    case Direction.Left: gridMoveDirectionVector = new Vector2Int(-1, 0); break;
                    case Direction.Up: gridMoveDirectionVector = new Vector2Int(0,+1); break;
                    case Direction.Down: gridMoveDirectionVector = new Vector2Int(0,-1); break;
            }
            
            gridPosition += gridMoveDirectionVector; // Позиция + движение в сторону.

            gridPosition = levelGrid.ValidateGridPosition(gridPosition); // Перемещение змейки.
            
            bool snakeEatFood = levelGrid.TrySnakeEatFood(gridPosition); // Передаём сетке свою позицию.
            if (snakeEatFood) // if true - body+1
            {
                snakeBodySize++;
                CreateSnakeBody();
            }

            if (snakeMovePositinList.Count >= snakeBodySize + 1) // Если список больше размера змеи.
            {   // Удаление последнего элемента списка.
                snakeMovePositinList.RemoveAt(snakeMovePositinList.Count - 1);
            }

            UpdateSnakeBodyParts(); // Обновление тела.
            
            // Проверяется положение головы и хвоста в сетке.
            // Если совпало, то игра завершается.
            foreach (SnakeBodyPart snakeBodyPart in snakeBodyPartList)
            {
                Vector2Int snakeBodyPartGridPosition = snakeBodyPart.GetGridPosition();
                if (gridPosition == snakeBodyPartGridPosition)
                {
                    //Game Over
                    Debug.Log("YOU DEAD!");
                    state = State.Dead;
                }
            }
            
            transform.position = new Vector3
                (gridPosition.x, gridPosition.y); // Изменение позиции змейки.
            transform.eulerAngles = new Vector3
                (0, 0, GetAngleFromVector(gridMoveDirectionVector) -90); // Изменение направления спрайта
                                            // по углу эйлера в взависимости от направления движения по Z.
                                            // -90 т.к начало змейки влево, а голова спрайта смотрит вверх.
        }
    }

    private void CreateSnakeBody()
    {
        snakeBodyPartList.Add(new SnakeBodyPart(snakeBodyPartList.Count));
    }

    private void UpdateSnakeBodyParts()
    {
        for (int i = 0; i < snakeBodyPartList.Count; i++)
        {
            // Из позиций списка тел берутся значения двух векторов x и y для постановки позиции тела.
            snakeBodyPartList[i].SetSnakeMovePosition(snakeMovePositinList[i]);
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
        foreach (SnakeMovePosition snakeMovePosition in snakeMovePositinList)
        {
            gridPositionList.Add(snakeMovePosition.GetGridPosition());
        }
        return gridPositionList;
    }
    
    /// <summary>
    /// Обработчик отдельной части тела змеи.
    /// </summary>
    private class SnakeBodyPart
    {
        private SnakeMovePosition snakeMovePosition;
        private Transform transform;
        public SnakeBodyPart(int bodyIndex)
        {
            // Создание и инициализация объекта с нужным именем и типом.
            GameObject snakeBodyGameObject = new GameObject("SnakeBody", typeof(SpriteRenderer));
            // Получение компонента
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.SnakeBodySprite;
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = bodyIndex;
            transform = snakeBodyGameObject.transform;
        }

        public void SetSnakeMovePosition(SnakeMovePosition snakeMovePosition)
        {
            this.snakeMovePosition = snakeMovePosition;
            transform.position = new Vector3(snakeMovePosition.GetGridPosition().x, snakeMovePosition.GetGridPosition().y);

            float angle;
            switch (snakeMovePosition.getDirection())
            {
                default:
                case Direction.Up:
                    switch (snakeMovePosition.GetPreviousDirection()) {
                        default:
                            angle = 0; break;
                        case Direction.Left:
                            angle = 0 + 45; break;
                        case Direction.Right:
                            angle = 0 - 45; break;
                    }
                    break;
                case Direction.Down:
                    switch (snakeMovePosition.GetPreviousDirection()) {
                        default:
                            angle = 180; break;
                        case Direction.Left:
                            angle = 180 + 45; break;
                        case Direction.Right:
                            angle = 180 - 45; break;
                    }
                    break;
                case Direction.Left:
                    switch (snakeMovePosition.GetPreviousDirection()) {
                        default:
                            angle = -90; break;
                        case Direction.Down:
                            angle = -45; break;
                        case Direction.Up:
                            angle = 45; break;
                    }
                    break;
                case Direction.Right:
                    switch (snakeMovePosition.GetPreviousDirection()) {
                        default:
                            angle = 90; break;
                        case Direction.Down:
                            angle = 45; break;
                        case Direction.Up:
                            angle = -45; break;
                    }
                    break;
            }        
            transform.eulerAngles = new Vector3(0, 0, angle);
        }

        public Vector2Int GetGridPosition()
        {
            return snakeMovePosition.GetGridPosition();
        }
    }

    private class SnakeMovePosition
    {
        private SnakeMovePosition previousSnakeMovePosition;
        private Vector2Int gridPosition;
        private Direction direction;

        public SnakeMovePosition(SnakeMovePosition previousSnakeMovePosition ,Vector2Int gridPosition, Direction direction)
        {
            this.previousSnakeMovePosition = previousSnakeMovePosition;
            this.gridPosition = gridPosition;
            this.direction = direction;
        }

        public Vector2Int GetGridPosition()
        {
            return gridPosition;
        }

        public Direction getDirection()
        {
            return direction;
        }

        public Direction GetPreviousDirection()
        {
            if (previousSnakeMovePosition == null)
            {
                return Direction.Right;
            }
            else
            {
                return previousSnakeMovePosition.direction;
            }
        }
    }
}
