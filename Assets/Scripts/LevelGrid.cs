using UnityEngine;

public class LevelGrid
{
    private Vector2Int foodGridPosition; // Позиция объекта (яблока).
    private GameObject foodGameObject;
    private int width; // Высота сетки.
    private int height; // Ширина сетки.
    private Snake snake; // Ссылка змеи, пометка.
    
    public LevelGrid(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    public void Setup(Snake snake) // Ссылка змеи, пометка.
    {
        this.snake = snake; // Ссылка змеи, пометка.
        SpawnFood();
    }
    
    /// <summary>
    /// Спавнер объетов (яблока).
    /// </summary>
    private void SpawnFood()
    {
        
        do { // Сначала идёт генерация, а потом проверка.
            // Генерация местоположения для игрового объекта (яблока).
            foodGridPosition = new Vector2Int(Random.Range(0,width), Random.Range(0, height));
            // Если позиции совпали, то новая проверка.
            // Если позиция совпадает, то перегрузка IndexOf возвращает индек в списке
            // и цикл повторяется, а если нет, то условия выполняются и 
        } while (snake.GetFullSnakeGridPositionList().IndexOf(foodGridPosition) != -1);

        // Создание объекта с именем и компонентом рендера спрайта.
        foodGameObject = new GameObject("Food", typeof(SpriteRenderer));
        foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.foodSprite; // Подкл. компонента.
        // Изменение позиции объекта (яблока).
        foodGameObject.transform.position = new Vector3Int(foodGridPosition.x, foodGridPosition.y); 
    }
    
    /// <summary>
    /// Метод проверки позиций змеи и еды между собой.
    /// </summary>
    /// <param name="snakeGridPosition"></param>
    public bool TrySnakeEatFood(Vector2Int snakeGridPosition)
    {
        if (snakeGridPosition == foodGridPosition) // Если позиция змеи и еды одинаковая
        {
            Object.Destroy(foodGameObject); // Удаление объекта еды.
            SpawnFood(); // Спавн новой.
            Debug.Log("Snake ate Food"); // Отладка.
            return true;
        }
        else
        {
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
            gridPosition.x = width - 1;
        }
        if (gridPosition.x > width - 1) {
            gridPosition.x = 0;
        }
        if (gridPosition.y < 0) {
            gridPosition.y = height - 1;
        }
        if (gridPosition.y > height - 1) {
            gridPosition.y = 0;
        }
        return gridPosition;
    }

}
