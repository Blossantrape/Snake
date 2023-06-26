using UnityEngine;

public class LevelGrid
{
    private Vector2Int _foodGridPosition; // Позиция объекта (яблока).
    private GameObject _foodGameObject;
    [HideInInspector] public readonly int _width; // Высота сетки.
    [HideInInspector] public readonly int _height; // Ширина сетки.
    private Snake _snake; // Ссылка змеи, пометка.
    
    public LevelGrid(int _width, int _height)
    {
        this._width = _width;
        this._height = _height;
    }

    public void Setup(Snake _snake) // Ссылка змеи, пометка.
    {
        this._snake = _snake; // Ссылка змеи, пометка.
        
        SpawnFood();
    }
    
    /// <summary>
    /// Спавнер объетов (яблока).
    /// </summary>
    private void SpawnFood()
    {
        
        do { // Сначала идёт генерация, а потом проверка.
            // Генерация местоположения для игрового объекта (яблока).
            _foodGridPosition = new Vector2Int(Random.Range(0,_width), Random.Range(0, _height));
            // Если позиции совпали, то новая проверка.
            // Если позиция совпадает, то перегрузка IndexOf возвращает индек в списке
            // и цикл повторяется, а если нет, то условия выполняются и 
        } while (_snake.GetFullSnakeGridPositionList().IndexOf(_foodGridPosition) != -1);

        // Создание объекта с именем и компонентом рендера спрайта.
        _foodGameObject = new GameObject("Food", typeof(SpriteRenderer));
        _foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.I.foodSprite; // Подкл. компонента.
        // Изменение позиции объекта (яблока).
        _foodGameObject.transform.position = new Vector3Int(_foodGridPosition.x, _foodGridPosition.y); 
    }
    
    /// <summary>
    /// Метод проверки позиций змеи и еды между собой.
    /// </summary>
    /// <param name="snakeGridPosition"></param>
    public bool TrySnakeEatFood(Vector2Int snakeGridPosition) {
            // Если позиция змеи и еды одинаковая
        if (snakeGridPosition == _foodGridPosition) {
            Object.Destroy(_foodGameObject); // Удаление объекта еды.
            SpawnFood(); // Спавн новой.
            GameHandler.AddScore();
            Debug.Log("Snake ate food"); // Отладка.
            return true;
        }
        else {
            return false;
        }
    }

    /// <summary>
    /// Если змейка ушла за край, то она телепортируется в противоположеное место.
    /// </summary>
    /// <param name="gridPosition"></param>
    /// <returns></returns>
    public Vector2Int ValidateGridPosition(Vector2Int gridPosition)
    {
        if (gridPosition.x < 0) {
            gridPosition.x = _width - 1;
        }
        if (gridPosition.x > _width - 1) {
            gridPosition.x = 0;
        }
        if (gridPosition.y < 0) {
            gridPosition.y = _height - 1;
        }
        if (gridPosition.y > _height - 1) {
            gridPosition.y = 0;
        }
        return gridPosition;
    }

}
