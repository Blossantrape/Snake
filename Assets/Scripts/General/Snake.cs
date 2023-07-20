using System.Collections.Generic;
using General;
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

    private State _state; //  Состояние змеи.
    private Direction _gridMoveDirection; // Направление змейки.
    private Vector2Int _gridPosition; // Позиция змейки.
    private float _gridMoveTimer; // Время для автоматического премещения змейки.
    private float _gridMoveTimerMax; // Её максимальное значение.
    private LevelGrid _levelGrid; // Ссылка змеи, пометка.
    private int _snakeBodySize; // Размер хвоста змеи.
    private List<SnakeMovePosition> _snakeMovePositionList; // 
    private List<SnakeBodyPart> _snakeBodyPartList;

    public void Setup(LevelGrid levelGrid) // Ссылка змеи, пометка.
    {
        this._levelGrid = levelGrid; // Ссылка змеи, пометка.
    }
    
    private void Awake() 
    {
        _gridPosition = new Vector2Int(0, 0); // Позция змейки, 0.9 - z, чтобы объект отображался.
        _gridMoveTimerMax = .5f; // Интервал движения.
        _gridMoveTimer = _gridMoveTimerMax; // Так надо.
        _gridMoveDirection = Direction.Right; // Направление змеи, вправо.

        _snakeMovePositionList = new List<SnakeMovePosition>(); // Инициализация списка.
        _snakeBodySize = 0; // Размер змеи
        _snakeBodyPartList = new List<SnakeBodyPart>(); // Инициализация списка.

        _state = State.Alive; // Стандартное живое состояние.
    }

    private void Update()
    {
        // Изменение режима игры в зависимости от состояния.
        switch (_state)
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
            if (_gridMoveDirection != Direction.Down) // Не позволяет поворачиваться на 180 градусов.
            {
                _gridMoveDirection = Direction.Up;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (_gridMoveDirection != Direction.Up) // Не позволяет поворачиваться на 180 градусов.
            {
                _gridMoveDirection = Direction.Down;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (_gridMoveDirection != Direction.Right) // Не позволяет поворачиваться на 180 градусов.
            {
                _gridMoveDirection = Direction.Left;
            }
            
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (_gridMoveDirection != Direction.Left) // Не позволяет поворачиваться на 180 градусов.
            {
                _gridMoveDirection = Direction.Right;
            }
        }
    }

    private void HandleGrindMovement()
    {
        _gridMoveTimer += Time.deltaTime; // Обновление таймера каждый кадр.
        if (_gridMoveTimer >= _gridMoveTimerMax) // Если время >= максимального, то цикл выполняется.
        {
            _gridMoveTimer -= _gridMoveTimerMax; // Не понял зачем.

            SoundManager.PlaySound(SoundManager.Sound.SnakeMove);

            SnakeMovePosition previousSnakeMovePosition = null;
            if (_snakeMovePositionList.Count > 0)
            {
                previousSnakeMovePosition = _snakeMovePositionList[0];
            }
            SnakeMovePosition snakeMovePosition = new SnakeMovePosition(previousSnakeMovePosition, _gridPosition, _gridMoveDirection);
            _snakeMovePositionList.Insert(0, snakeMovePosition);

            Vector2Int gridMoveDirectionVector;
            switch (_gridMoveDirection)
            {
                default:
                    case Direction.Right: gridMoveDirectionVector = new Vector2Int(+1, 0); break;
                    case Direction.Left: gridMoveDirectionVector = new Vector2Int(-1, 0); break;
                    case Direction.Up: gridMoveDirectionVector = new Vector2Int(0,+1); break;
                    case Direction.Down: gridMoveDirectionVector = new Vector2Int(0,-1); break;
            }
            
            _gridPosition += gridMoveDirectionVector; // Позиция + движение в сторону.

            _gridPosition = _levelGrid.ValidateGridPosition(_gridPosition); // Перемещение змейки.
            
            bool snakeEatFood = _levelGrid.TrySnakeEatFood(_gridPosition); // Передаём сетке свою позицию.
            if (snakeEatFood) // if true - body+1
            {
                _snakeBodySize++;
                CreateSnakeBody();
                SoundManager.PlaySound(SoundManager.Sound.SnakeEat);
            }

            if (_snakeMovePositionList.Count >= _snakeBodySize + 1) // Если список больше размера змеи.
            {   // Удаление последнего элемента списка.
                _snakeMovePositionList.RemoveAt(_snakeMovePositionList.Count - 1);
            }

            UpdateSnakeBodyParts(); // Обновление тела.
            
            // Проверяется положение головы и хвоста в сетке.
            // Если совпало, то игра завершается.
            foreach (SnakeBodyPart snakeBodyPart in _snakeBodyPartList)
            {
                Vector2Int snakeBodyPartGridPosition = snakeBodyPart.GetGridPosition();
                if (_gridPosition == snakeBodyPartGridPosition)
                {
                    //Game Over
                    Debug.Log("YOU DEAD!");
                    _state = State.Dead;
                    GameHandler.SnakeDied();
                    SoundManager.PlaySound(SoundManager.Sound.SnakeDie);
                    SoundManager.PlaySound(SoundManager.Sound.GameOver);
                }
            }
            
            transform.position = new Vector3
                (_gridPosition.x, _gridPosition.y); // Изменение позиции змейки.
            transform.eulerAngles = new Vector3
                (0, 0, GetAngleFromVector(gridMoveDirectionVector) -90); // Изменение направления спрайта
                                            // по углу эйлера в взависимости от направления движения по Z.
                                            // -90 т.к начало змейки влево, а голова спрайта смотрит вверх.
        }
    }

    private void CreateSnakeBody()
    {
        _snakeBodyPartList.Add(new SnakeBodyPart(_snakeBodyPartList.Count));
    }

    private void UpdateSnakeBodyParts()
    {
        for (int i = 0; i < _snakeBodyPartList.Count; i++)
        {
            // Из позиций списка тел берутся значения двух векторов x и y для постановки позиции тела.
            _snakeBodyPartList[i].SetSnakeMovePosition(_snakeMovePositionList[i]);
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
        return _gridPosition;
    }

    // Возвраает полный список позиций окупаемые змеёй: голова и хвост.
    public List<Vector2Int> GetFullSnakeGridPositionList()
    {
        List<Vector2Int> gridPositionList = new List<Vector2Int>() { _gridPosition };
        foreach (SnakeMovePosition snakeMovePosition in _snakeMovePositionList)
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
        private SnakeMovePosition _snakeMovePosition;
        private readonly Transform _transform;
        public SnakeBodyPart(int bodyIndex)
        {
            // Создание и инициализация объекта с нужным именем и типом.
            GameObject snakeBodyGameObject = new GameObject("SnakeBody", typeof(SpriteRenderer));
            // Получение компонента
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.I.snakeBodySprite;
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = bodyIndex;
            _transform = snakeBodyGameObject.transform;
        }

        public void SetSnakeMovePosition(SnakeMovePosition snakeMovePosition)
        {
            this._snakeMovePosition = snakeMovePosition;
            _transform.position = new Vector3(snakeMovePosition.GetGridPosition().x, snakeMovePosition.GetGridPosition().y);

            float angle;
            switch (snakeMovePosition.GetDirection())
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
            _transform.eulerAngles = new Vector3(0, 0, angle);
        }

        public Vector2Int GetGridPosition()
        {
            return _snakeMovePosition.GetGridPosition();
        }
    }

    private class SnakeMovePosition
    {
        private readonly SnakeMovePosition _previousSnakeMovePosition;
        private readonly Vector2Int _gridPosition;
        private readonly Direction _direction;

        public SnakeMovePosition(SnakeMovePosition _previousSnakeMovePosition, Vector2Int _gridPosition, Direction _direction)
        {
            this._previousSnakeMovePosition = _previousSnakeMovePosition;
            this._gridPosition = _gridPosition;
            this._direction = _direction;
        }

        public Vector2Int GetGridPosition()
        {
            return _gridPosition;
        }

        public Direction GetDirection()
        {
            return _direction;
        }

        public Direction GetPreviousDirection()
        {
            if (_previousSnakeMovePosition == null)
            {
                return Direction.Right;
            }
            else
            {
                return _previousSnakeMovePosition._direction;
            }
        }
    }
}
